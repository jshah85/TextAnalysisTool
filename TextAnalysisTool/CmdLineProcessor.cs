
namespace TextAnalysisTool {
    class CmdLineProcessor
    {

        private readonly string _commandLine;

        public CmdLineProcessor(string commandLine) {
            _commandLine = commandLine;
            ProcessCommandLine();
        }

        private void ProcessCommandLine() {
            if (_commandLine != null) {

                var tokens = _commandLine.Split(' ');
                Command = tokens[0];

                if (Command == "analyze") {
                    if (tokens.Length == 2) {
                        EssayName = tokens[1];
                    }
                    else if (tokens.Length > 2) {
                        Sentence = string.Join(" ", tokens, 1, tokens.Length - 1);
                    }
                }
                else if (Command == "define") {
                    Word = tokens[1];
                }
            }
        }

        public string Command { get; private set; }

        public string Sentence { get; private set; }

        public string EssayName { get; private set; }

        public string Word { get; private set; }
    }
}
