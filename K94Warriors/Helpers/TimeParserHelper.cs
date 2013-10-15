using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace K94Warriors.Helpers
{
    public class TimeParserHelper
    {
        public static TimeSpan? Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            input = input.Trim();

            if (input.Length > 10)
                return null; // prevent runaway call stacks

            var fsm = new TimeParserFiniteStateMachine(input);

            return fsm.Parse();
        }

        private class TimeParserFiniteStateMachine
        {
            private readonly CharStream stream;
            private const int zeroChar = (int)'0';
            private const char EOF = (char)0;

            public TimeParserFiniteStateMachine(string input)
            {
                stream = new CharStream(input);
            }

            public TimeSpan? Parse()
            {
                var state = new TimeState();
                return LookForHourChar(state);
            }

            public TimeSpan? Finish(TimeState state)
            {
                return state.IsComplete ? state.ToTimeSpan() : null;
            }

            private TimeSpan? LookForHourChar(TimeState state, bool beenHere = false)
            {
                char c = stream.ReadChar();

                if (c == EOF)
                    return beenHere && state.IsComplete ? state.ToTimeSpan() : null;

                switch (c)
                {
                    case '0':
                    case '1':
                    case '2':
                        if (beenHere)
                        {
                            state.Hour *= 10;
                            state.Hour += ((int)c) - zeroChar;
                            return LookForDelimiter(state);
                        }
                        else
                        {
                            state.Hour = ((int)c) - zeroChar;
                            return LookForHourChar(state, true);
                        }
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        if (beenHere)
                        {
                            state.Hour *= 10;
                            state.Hour += ((int)c) - zeroChar;
                        }
                        else
                            state.Hour = ((int)c) - zeroChar;

                        return LookForDelimiter(state);
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                        return LookForHourChar(state, false);
                    default:
                        return null;
                }
            }

            private TimeSpan? LookForDelimiter(TimeState state)
            {
                char c = stream.Peek(0);

                if (c == EOF)
                    return Finish(state);

                switch (c)
                {
                    case ':':
                        stream.ReadChar();
                        return LookForMinuteChar(state);
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        return LookForMinuteChar(state);
                    case 'p':
                    case 'P':
                    case 'a':
                    case 'A':
                        return LookForAmPm(state);
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                        stream.ReadChar();
                        return LookForDelimiter(state);
                    default:
                        return null;
                }
            }

            private TimeSpan? LookForAmPm(TimeState state)
            {
                char c = stream.ReadChar();

                if (c == EOF)
                    return Finish(state);

                switch (c)
                {
                    case 'a':
                    case 'A':
                        state.Is24Hour = false;
                        state.IsPM = false;
                        return LookForAmPmEnd(state);
                    case 'p':
                    case 'P':
                        state.Is24Hour = false;
                        state.IsPM = true;
                        return LookForAmPmEnd(state);
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                        return LookForAmPm(state);
                    default:
                        return null;
                }
            }

            private TimeSpan? LookForAmPmEnd(TimeState state)
            {
                char c = stream.ReadChar();

                if (c == EOF)
                    return Finish(state);

                switch (c)
                {
                    case 'm':
                    case 'M':
                        return Finish(state);
                    default:
                        return null;
                }
            }

            private TimeSpan? LookForMinuteChar(TimeState state, bool beenHere = false)
            {
                char c = stream.ReadChar();

                if (c == EOF)
                    return beenHere && state.IsComplete ? state.ToTimeSpan() : null;

                switch (c)
                {
                    case 'a':
                    case 'A':
                    case 'p':
                    case 'P':
                        stream.Backup(1);
                        return LookForAmPm(state);
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                        return LookForMinuteChar(state, false);
                    case '0':
                    case '1':
                    case '2': 
                    case '3':
                    case '4':
                    case '5':
                        if (beenHere)
                        {
                            state.Minute *= 10;
                            state.Minute += ((int)c) - zeroChar;
                            return LookForAmPm(state);
                        }
                        else
                        {
                            state.Minute = ((int)c) - zeroChar;
                            return LookForMinuteChar(state, true);
                        }
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        if (!beenHere)
                            return null; // minutes can't start with 6-9

                        state.Minute *= 10;
                        state.Minute += ((int)c) - zeroChar;

                        return LookForAmPm(state);
                    default:
                        return null; // invalid character
                }
            }
        }

        private class CharStream
        {
            private string source = null;
            private int pos = 0;

            public CharStream(string source)
            {
                this.source = source;
            }

            public char ReadChar()
            {
                if (pos >= source.Length)
                    return (char)0;

                return source[pos++];
            }
            
            public void Backup(int amount)
            {
                if (pos - amount < 0)
                    pos = 0;
                else
                    pos -= amount;
            }

            public char Peek(int distance = 1)
            {
                if (pos + distance >= source.Length)
                    return (char)0;

                return source[pos + distance];
            }
        }

        private class TimeState
        {
            public int? Hour;
            public int? Minute;
            public bool? IsPM;
            public bool Is24Hour = true;

            public bool IsComplete
            {
                get
                {
                    // as long as we at least have an hour, we can infer some timespan
                    return Hour.HasValue;
                }
            }
            
            public TimeSpan? ToTimeSpan()
            {
                if (!IsComplete)
                    return null;

                int hour = Hour.GetValueOrDefault();
                int minute = Minute.GetValueOrDefault();
                bool isPM = IsPM.GetValueOrDefault();

                if (Is24Hour)
                    return TimeSpan.FromHours(hour).Add(TimeSpan.FromMinutes(minute));
                
                if (isPM)
                {
                    if (hour == 12)
                        return TimeSpan.FromHours(hour).Add(TimeSpan.FromMinutes(minute));

                    return TimeSpan.FromHours(12 + hour).Add(TimeSpan.FromMinutes(minute));
                }

                if (hour == 12)
                    return TimeSpan.FromMinutes(minute);

                return TimeSpan.FromHours(hour).Add(TimeSpan.FromMinutes(minute));
            }
        }
    }
}