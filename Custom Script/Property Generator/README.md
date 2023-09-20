First put Unit.prefab in the same directory
Then open window and select Property Generator
All pretty self-explanatory. However instead select a faction folder, this time you need to select one specific faction you want to add property to it
For unit, it just a folder that have unit inside. You can add your own modded ship in it
Choose the amount of unit that you want to add to the faction

# Important! 
-Make sure the Unit folder that contained ship and station is outside the savegame so it doesn`t included in the exported file.
-Make sure the faction(especially player if you want to add random unit for player) to have homesector selected. This is because the PropertyGenerator use homesector as the location to generate the units.


# Note:
-Yes you can add any kind unit besides ships in the unit prefab like stations, cargo, etc.
-Any kind unit besides ships may need repositioning as it will generate the unit at center of sector.
-Be careful with the number, generating too much may cause lag in the editor.

This as far as I can do now, it is possible to auto generate an amount of property to distributed to all faction in all sector but that requires an a very high level of advanced algorithms that probably make me shoot myself
