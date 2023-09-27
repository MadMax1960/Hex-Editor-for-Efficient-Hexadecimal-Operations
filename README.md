# Hee-Ho (Hex Editor for Efficient Hexadecimal Operations)

Hee-Ho is a simple C# program to enable batch hex editing in a fast way. This program is targeted towards replacing something like a 010 script for something more versatile and straight forward. 


## Installation

Download .Net 6.0 [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

Download the latest release. 


    
## Usage

First up you need to have a `default.json`, or (anything).json that's structured like this 

```{
  "ReplacementsList": [
    {
      "SearchHex": "00 09 52 5F 62 75 73 74 5F 30 30 FF 97 3D 79",
      "ReplaceHex": "00 0A 52 5F 63 72 69 6E 67 5F 30 30 13 CC 19 97"
    },
    {
      "SearchHex": "00 09 4C 5F 62 75 73 74 5F 30 30 9B 95 A4 9A",
      "ReplaceHex": "00 0A 4C 5F 63 72 69 6E 67 5F 30 30 2A AB A8 CC"
    },
    {
      "SearchHex": "00 06 43 5F 62 75 73 74 75 B9 C9 E6",
      "ReplaceHex": "00 06 46 75 63 6B 75 70 51 36 E5 70"
    }
  ]
}
```

If it's not very clear, you need to put the hex you're searching for in `SearchHex` and the hex you're replacing with in `ReplaceHex`. The program only searches for whole matches, meaning if you're trying to partially replace some search with replace, it won't work. It will just insert the replace hex if it finds all of the entry of SearchHex.

To actually utalize the program, make a default.json and just drag a file or folder onto the exe. However, for batch purposes, make a .json named whatever you want, and use the program like this. 

`"Hex Editor for Efficient Hexadecimal Operations.exe" [filepath] [json]`

If needed, you can rename the executible to whatever you want, in order to make batch/python scripts less messy. Or because windows decided to die on you and won't read the exe due to spaces instead of underscored. 


## Examples

An example use for this program is the preparation of persona 5 models for retargetting. Some persona 5 models use `Bip01 LThighTwist`, while some use `Bip01 L ThighTwist`. This repeats for the right thightwist, on top of thightwist 1. 

This ends up breaking GFD Studio retargetting, so you would need to rename either all the models bones, or all the animations, or all the models and animations. Doing this by hand, or even with a 010 batch, is still a bit tedious. 

So to automate it, I have a json setup like this 

```{
  "ReplacementsList": [
    {
      "SearchHex": "00 11 42 69 70 30 31 20 4C 54 68 69 67 68 54 77 69 73 74",
      "ReplaceHex": "00 12 42 69 70 30 31 20 4C 20 54 68 69 67 68 54 77 69 73 74"
    },
    {
      "SearchHex": "00 12 42 69 70 30 31 20 4C 54 68 69 67 68 54 77 69 73 74 31",
      "ReplaceHex": "00 13 42 69 70 30 31 20 4C 20 54 68 69 67 68 54 77 69 73 74 31"
    },
    {
      "SearchHex": "00 11 42 69 70 30 31 20 52 54 68 69 67 68 54 77 69 73 74",
      "ReplaceHex": "00 12 42 69 70 30 31 20 52 20 54 68 69 67 68 54 77 69 73 74"
    },
    {
      "SearchHex": "00 12 42 69 70 30 31 20 52 54 68 69 67 68 54 77 69 73 74 31",
      "ReplaceHex": "00 13 42 69 70 30 31 20 52 20 54 68 69 67 68 54 77 69 73 74 31"
    }
  ]
}
```

This will change every thightwist, and thightwist 1, to be properly formatted for the majority of other models. The way I would use this for instance, would be. 

`"Hex Editor for Efficient Hexadecimal Operations.exe" I:\balls\model\character\0002 retarget.json`

This would rename all of ryuji's bones in his models to be properly formatted. I could repeat this process recursively via batch to every subfolder and file within those folders, or do it one by one. Or, rename `retarget.json` to `default.json` and just drag said folders over the exe. 



## FAQ

#### The files seem to be the same, what's going on?

If the console showed if went through all the files, double check your json is formatted properly. It will only replace whole cases, meaning it will ignore partial matches. 

#### The file isn't functioning how I want it to now, what do I do?

This is a case to case basis, but in some cases entrys in a file might have a filesize stored somewhere. For instance if you hex edit a texture in, there might be a check for the size of it somewhere, you would need to append that too. 

