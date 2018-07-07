# Text Analysis Tool
C# console application that mimicks Command Line tool to analyze input text or essay. It recognizes below commands
  - help  - shows help about below commands
  - analyze <sentence|essayfile>- main command to analyze either text or essay. User provides the name of essay file
  - define <word> - shows definition of input word
  - exit - close running app

## How To Run
Application will keep running as command line mode until stopped by exit command. Program.cs has entry method main().
- `analyze <sentence>`
  -	User types in a freeform sentence.
- `analyze <essayfile>`
  -	User proivdes essary file name available at ~/Corpus/EssayFiles.
- `define <word>`
  -	User types in a word from sentence or essay used.

## Analysis Output
Using the word lists from the References section, generate the following statistics:
- List and count words in short-length category.
- List and count words in medium-length category.
- List and count words in long-length category.
- List and count words that were not found in any of the lists.



## References
1. Reference word lists, including length categorization: https://github.com/first20hours/google-10000-english
2. Word meanings dictionary: http://www.mso.anu.edu.au/~ralph/OPTED/
3. Sample essays from here: https://github.com/xaviervia/essays
