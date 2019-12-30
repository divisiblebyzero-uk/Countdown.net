[![Build Status](https://travis-ci.com/divisiblebyzero-uk/Countdown.net.svg?branch=master)](https://travis-ci.com/divisiblebyzero-uk/Countdown.net)

# Countdown.net

.net implementation of the [Countdown solver](https://github.com/pinkius/countdown) originally written in Java.

This project is a WebAPI only - a separate UI would be needed to make this a user-friendly tool.

The solver is split into two parts: WordSearch and NumbersSolver. Both have controllers in the Controllers folder.

## WordSearch

The WordSearch works using the following algorithm:

1. Initialisation
   1. A corpus of words in the English language is downloaded from the configured URL (see [appsettings.json](Countdown.net/appsettings.json)), which by default is https://raw.githubusercontent.com/dwyl/english-words/master/words_alpha.txt
   2. This corpus is digested into an enumeration of WordCount objects. Each [WordCount](Countdown.net/Model/WordCount.cs) object contains the string itself, as well as a Dictionary holding the count of each letter.
 
      For instance, the word "Hello" would be converted into 

```json
{
    "TheWord": "hello",
    "Length": 5,
    "LetterCount": {
        "l": 2,
        "e": 1,
        "h": 1,
        "o": 1
    }
}
```
  3. Finally, this set of WordCount objects is held by the [WordSearchController](Countdown.net/Controllers/WordSearchController.cs), and can be downloaded from the running API by hitting URL https://host/api/WordSearch/
2. The search
   1. The search is initiated by GETing https://host/api/WordSearch/LETTERS, where LETTERS are the specific letters given in the game.
   2. The application converts LETTERS into a [WordCount](Countdown.net/Model/WordCount.cs) object.
   3. The application than iterates over every [WordCount](Countdown.net/Model/WordCount.cs) in the corpus, comparing the needed number of each letter with that available in the LETTERS, adding successful matches to a list of strings.
```c#
foreach (WordCount wordCount in Words.Values)
{
    bool matched = true;
    foreach (char d in wordCount.LetterCount.Keys)
    {
        if (!wc.LetterCount.ContainsKey(d) || wc.LetterCount[d] < wordCount.LetterCount[d])
        {
            matched = false;
            break;
        }
    }

    if (matched)
    {
        matchList.Add(wordCount.TheWord);
    }
}
```

    4. The matchlist is then sorted by size (score) and returned, along with a Long representing time in milliseconds taken to perform the search.

## NumbersSolver

The numbers solver is a little trickier. To represent a potential solution, the class NumbersSolution has a tree of nodes, which are either IntegerNodes (numbers) or CalculationNodes (operators).

An IntegerNode contains just an integer number. A CalculationNode contains two Nodes (Left Node and Right Node) and an operator, which is one of:
* Add
* Subtract
* Multiply
* Divide
* Ignore Left
* Ignore Right

The result of any calculation node MUST itself be an integer - it is an invalid node if it results in a non-integer number. So for instance, the following calculation node would be invalid:
```json
{
    "leftNode": 3,
    "rightNode": 2,
    "operator": "Divide"
}
```

The algorithm for finding the solution is as follows:

1. The NumbersSolverController takes in a list of numbers, the first of which is the target. For example, 100,10,6,3.
2. After chopping the target, the remaining numbers are converted into an enumerable of all permutations (in this case, 3,6,10 , 3,10,6 , 6,3,10 , 6,10,3 , 10,3,6 , 10,6,3).
3. Each permutation is taken in turn, and a method is called recursively: if the count of numbers passed in is 1, then a single Integer node is given back. If more than one number is passed in, then the first number is combined with every type of operator and the remaining list.
4. This results in a set of node trees (calculations) per pemutation of the input list. This list of sets is then ordered by closeness to the target, and then by complexity.
   NB Complexity of an Integer node is 1. Complexity of a Calculation Node is 1 + Complexity of left node + Complexity of right node.
5. The results are then played back via a NumbersSolutionDto object. 