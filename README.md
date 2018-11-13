# Edi.WordFilter

Module used in my blog system to filter disharmony words in order to live in China.

It uses hashtable to filter content very quickly.

## Usage
1. Prepare a text file with banned words, for example splitted by "|". Like this:
```
翻墙|防火长城|GFW|fuck
```
Because I still want to live, I can't give you the entire keywords list, sorry for that.

2. Install from NuGet: Install-Package Edi.WordFilter

3. Use it like this:
```
var wordFilterDataFilePath = $"{AppDomain.CurrentDomain.GetData(Constants.DataDirectory)}\\BannedWords.txt";
var maskWordFilter = new MaskWordFilter(wordFilterDataFilePath);
username = maskWordFilter.FilterContent(username);
commentContent = maskWordFilter.FilterContent(commentContent);
```

4. The disharmony words will be replaced by "*"

### Design Details

Split disharmony word into HashTable, Key points to the first character, Value points to the next character where Value itself is the Key of the next HashTable. When filtering content, begin search with the first HashTable, if matching double side, then it is a disharmony word.

For example, given disharmony word "FUCK,FS,ABC", the following structure is created:

// TODO

Each black box represents a HashTable, each character of the disharmony word is stored as the Key and pointing to the next HashTable.

For example, if user input "FUCK FAKE", the flow is:

"F" can be found in the first level of HashTable(H1), "U" can be found in the HashTable(H2) where H1's value is reffered to, like this, C and K can be found in HashTable(H3) and HashTable(H4), so "FUCK" is a disharmony word.

For the word "FAKE", although "F" can be found in HashTable(H1), but H1 does not have a pointer to "A", and "A" sits // TODO
