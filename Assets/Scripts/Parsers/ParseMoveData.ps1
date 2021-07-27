# Given the name of an XML node, finds unique instances of that node's value within the contents of the move XML files in the current directory.
# Formats the results as a Markdown bulleted list, in the format "* value // # files // filenames..."
# Output is placed in a text file in the parent directory.
#
# Example usage (within ./move_data/):
# 9..19 | % { Parse-Move-Data ('Unk' + $_) }
# Parse-Move-Data 'Unk4'
# Parse-Move-Data 'Accuracy'
function Parse-Move-Data
{
    param (
        [Parameter(ValueFromPipeline=$true)]
        [String[]]
        $fields
    )
    foreach ($field in $fields)
    {
        $foundFieldReplaceStr = '.*<field>(?<val>\d+)</field>.*' -replace 'field', $field
        $foundFieldResults = Get-ChildItem | Select-String -SimpleMatch -Pattern ('<' + $field + '>')
        $uniqueValues = New-Object 'System.Collections.Generic.HashSet[string]'
        foreach ($found in $foundFieldResults)
        {
            $val = $found -replace $foundFieldReplaceStr, '${val}'
            $uniqueValues.Add($val) >$null
        }
        # Output to the parent directory so that repeat calls don't find different results
        $dest = '../field.txt' -replace 'field', $field
        Clear-Content $dest
        foreach ($value in $uniqueValues)
        {
            $toFind = $field + '>' + $value + '<'
            $filesWithValue = Get-ChildItem | Select-String -SimpleMatch -Pattern $toFind -List
            $uniqueMoves = New-Object 'System.Collections.Generic.HashSet[string]'
            foreach ($file in $filesWithValue)
            {
                $moveNamePart = $file -replace '.*\d+_([a-zA-z_\d]+)\.xml:.*', '$1'
                $uniqueMoves.Add($moveNamePart) >$null
            }
            $numberOfFiles = $filesWithValue | Measure-Object | Select-Object -ExpandProperty Count
            $fileOrFiles = 'files'
            if ($numberOfFiles -eq 1) {
                $fileOrFiles = 'file'
            }
            $output = '* ' + $value + ' // ' + $numberOfFiles + ' ' + $fileOrFiles + ' // '
            foreach ($move in $uniqueMoves)
            {
                $output += ($move + ', ')
            }
            $output >> $dest
        }
    }
}