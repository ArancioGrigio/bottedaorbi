timeout /t 3
move characters\temp\*.txt characters
move sprites\temp\*.png sprites
move sprites\temp\*.jpg sprites
move sprites\temp\*.jpeg sprites
move sounds\temp\*.wav sounds
move sounds\temp\*.mp3 sounds
del characters.exe
del sprites.exe
del sounds.exe
rmdir characters\temp
rmdir sprites\temp
rmdir sounds\temp
