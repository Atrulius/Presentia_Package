This is a package for C# Windows Forms

This package helps you use the file extensions:
.ptia (Short for Presentia) and
.ptie (.ptia but E for Editor)

This package has a namespace named: "Type"
In this namespace, there are four classes, one for each type of presentia there is.
PTIA: Presentia presentation for viewing
PTIA_File: Same as PTIA, but unnecessary information removed, and only contains what's needed in a file
PTIE: Presentia presentation for editing (Contains import information, path information and more for the editor)
PTIE_File: Same as PTIE, but again, unnecessary information removed.

This package also has a static class, filled with functions, named: "Convert"

Convert lets you convert between different types. So for example, if you want to
export an editor version of the presentation, you can do: Convert.PTIE_To_PTIE_File.