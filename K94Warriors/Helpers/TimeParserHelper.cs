using System;

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
            private readonly CharStream _stream;
            private const int ZERO_CHAR = '0';
            private const char EOF = (char)0;

            public TimeParserFiniteStateMachine(string input)
            {
                _stream = new CharStream(input);
            }

            public TimeSpan? Parse()
            {
                var state = new TimeState();
                return LookForHourChar(state);
            }

            private static TimeSpan? Finish(TimeState state)
            {
                return state.IsComplete ? state.ToTimeSpan() : null;
            }

            private TimeSpan? LookForHourChar(TimeState state, bool beenHere = false)
            {
                while (true)
                {
                    char c = _stream.ReadChar();

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
                                state.Hour += c - ZERO_CHAR;
                                return LookForDelimiter(state);
                            }
                            else
                            {
                                state.Hour = c - ZERO_CHAR;
                                beenHere = true;
                                continue;
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
                                state.Hour += c - ZERO_CHAR;
                            }
                            else
                                state.Hour = c - ZERO_CHAR;

                            return LookForDelimiter(state);
                        case ' ':
                        case '\t':
                        case '\r':
                        case '\n':
                            beenHere = false;
                            continue;
                        default:
                            return null;
                    }
                }
            }

            private TimeSpan? LookForDelimiter(TimeState state)
            {
                while (true)
                {
                    char c = _stream.Peek(0);

                    if (c == EOF)
                        return Finish(state);

                    switch (c)
                    {
                        case ':':
                            _stream.ReadChar();
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
                            _stream.ReadChar();
                            continue;
                        default:
                            return null;
                    }
                }
            }

            private TimeSpan? LookForAmPm(TimeState state)
            {
                while (true)
                {
                    char c = _stream.ReadChar();

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
                            continue;
                        default:
                            return null;
                    }
                }
            }

            private TimeSpan? LookForAmPmEnd(TimeState state)
            {
                char c = _stream.ReadChar();

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
                while (true)
                {
                    char c = _stream.ReadChar();

                    if (c == EOF)
                        return beenHere && state.IsComplete ? state.ToTimeSpan() : null;

                    switch (c)
                    {
                        case 'a':
                        case 'A':
                        case 'p':
                        case 'P':
                            _stream.Backup(1);
                            return LookForAmPm(state);
                        case ' ':
                        case '\t':
                        case '\r':
                        case '\n':
                            beenHere = false;
                            continue;
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                            if (beenHere)
                            {
                                state.Minute *= 10;
                                state.Minute += c - ZERO_CHAR;
                                return LookForAmPm(state);
                            }
                            else
                            {
                                state.Minute = c - ZERO_CHAR;
                                beenHere = true;
                                continue;
                            }
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            if (!beenHere)
                                return null; // minutes can't start with 6-9

                            state.Minute *= 10;
                            state.Minute += c - ZERO_CHAR;

                            return LookForAmPm(state);
                        default:
                            return null; // invalid character
                    }
                }
            }
        }

        private class CharStream
        {
            private readonly string _source;
            private int _pos;

            public CharStream(string source)
            {
                _source = source;
            }

            public char ReadChar()
            {
                if (_pos >= _source.Length)
                    return (char)0;

                return _source[_pos++];
            }
            
            public void Backup(int amount)
            {
                if (_pos - amount < 0)
                    _pos = 0;
                else
                    _pos -= amount;
            }

            public char Peek(int distance = 1)
            {
                if (_pos + distance >= _source.Length)
                    return (char)0;

                return _source[_pos + distance];
            }
        }

        private class TimeState
        {
            public int? Hour;
            public int? Minute;
            public bool? IsPM;
            public bool Is24Hour = true;

            public bool IsComplete => Hour.HasValue;

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
                    return hour == 12 
                        ? TimeSpan.FromHours(hour).Add(TimeSpan.FromMinutes(minute)) 
                        : TimeSpan.FromHours(12 + hour).Add(TimeSpan.FromMinutes(minute));
                }

                return hour == 12 
                    ? TimeSpan.FromMinutes(minute) 
                    : TimeSpan.FromHours(hour).Add(TimeSpan.FromMinutes(minute));
            }
        }
    }
}