First make sure NameLibrary.cs is in the same directory with GenerateFaction.cs
Then assign each object
Then adjust the amount of faction you want to have

# Notes:
 All generated faction have random stats and pre-assigned homesector.
 If you can do little coding, you can expand NameLibrary.cs by adding your own word.
 There is plenty comment inside NameLibrary.cs to help you out, but in general:
  There is 3 section each divided into 2 part.

   First section is for virtue value.
   It have two part. Front name and Rear name.
   Each word must have a value.
   Higher = More toward good, lower = More toward evil.
   The min and max value is -5 and 5 .
   It use simple "feelings" as if you think the name is
   sounds from something good then it have higher value and
   vice versa.

  -Second section is for aggressiveness value.
  -It have two part. Front name and Rear name.
  -Each word must have a value.
  -Higher = more aggressive, lower = less aggressive.
  -The min and max value is -4 and 5 , Don't ask why
  -It use simple "feelings" as if you think the name is
   sounds something aggressive then it have higher value
   and vice versa.

  -Third section is just the library.
  -It have two section. Front name and Rear name.
  -Just the name.

For example if you want to add new front name then all three section for the front name must be added
