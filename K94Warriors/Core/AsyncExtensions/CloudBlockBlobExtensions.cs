using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;

namespace K94Warriors.Core.AsyncExtensions
{
    public static class CloudBlockBlobExtensions
    {
        public static async Task<T> DownloadToStreamAsync<T>(this CloudBlockBlob blob, T stream)
            where T : Stream
        {
            var tcs = new TaskCompletionSource<T>();
            blob.BeginDownloadToStream(stream, iar =>
                {
                    try { blob.EndDownloadToStream(iar); tcs.TrySetResult(stream); }
                    catch (OperationCanceledException) { tcs.TrySetCanceled(); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                }, null);
            return await tcs.Task;
        }

        public static async Task UploadFromStreamAsync<T>(this CloudBlockBlob blob, T stream)
            where T : Stream
        {
            var tcs = new TaskCompletionSource<object>();
            blob.BeginUploadFromStream(stream, iar =>
                {
                    try { blob.EndDownloadToStream(iar); tcs.TrySetResult(null); }
                    catch (OperationCanceledException) { tcs.TrySetCanceled(); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                }, null);
            await tcs.Task;
        }

        public static async Task DeleteAsync(this CloudBlockBlob blob)
        {
            var tcs = new TaskCompletionSource<object>();
            blob.BeginDelete(iar =>
                {
                    try { blob.EndDownloadToStream(iar); tcs.TrySetResult(null); }
                    catch (OperationCanceledException) { tcs.TrySetCanceled(); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                }, null);
            await tcs.Task;
        }

        public static async Task<bool> DeleteIfExistsAsync(this CloudBlockBlob blob)
        {
            var tcs = new TaskCompletionSource<bool>();
            blob.BeginDeleteIfExists(iar =>
                {
                    try { tcs.TrySetResult(blob.EndDeleteIfExists(iar)); }
                    catch (OperationCanceledException) { tcs.TrySetCanceled(); }
                    catch (Exception ex) { tcs.TrySetException(ex); }
                }, null);
            return await tcs.Task;
        }
    }
}