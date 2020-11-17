# PasteToFile
Clipboard saver

Checks the content of the clipboard on activation and allows to save the content in one of supported formats.

Formats supported so far:

1. Plain text (txt)
2. Comma Separated Values (csv)
3. Tab Separated Values (converts to CSV)
4. Images (JPG/PNG)
5. SQL Server Management Studio query execution results formatted as text (converts to CSV)

Example of the latter:

```
Id          Title                                              Year        ImdbRating    ImdbNumVotes RuntimeMinutes
----------- -------------------------------------------------- ----------- ------------- ------------ --------------
1634106     Bloodshot                                          2020        5.7           30710        109
5774060     Underwater                                         2020        5.9           32425        95
9354842     To All the Boys: P.S. I Still Love You             2020        6             16663        101
983946      Fantasy Island                                     2020        4.8           13199        109
7146812     Onward                                             2020        7.5           47778        102
8244784     The Hunt                                           2020        6.4           28391        90
9086228     Gretel & Hansel                                    2020        5.3           10133        87
9139220     Dracula                                            2020        6.8           36273        270
3111426     Lost Girls                                         2020        6.1           10990        95
7713068     Birds of Prey: And the Fantabulous Emancipation of 2020        6.2           98920        109

(10 rows affected)

Id          Name
----------- --------------------------------------------------
2           Romance
3           Animation
4           Family
5           Horror
6           Comedy
7           Sci-Fi
8           Musical
9           Mystery
10          Fantasy
11          Adventure

(10 rows affected)


Completion time: 2020-11-16T21:01:10.9572129-05:00

```
