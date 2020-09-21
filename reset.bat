del characters\*.txt
copy temp\backup\characters\*.txt characters
del sprites\*.png
del sprites\*.jpg
del sprites\*.jpeg
copy temp\backup\sprites\*.png sprites
copy temp\backup\sprites\*.jpg sprites
copy temp\backup\sprites\*.jpeg sprites
del sounds\*.wav
del sounds\*.mp3
copy temp\backup\sounds\*.wav sounds
copy temp\backup\sounds\*.mp3 sounds

rmdir characters\temp
rmdir sprites\temp
rmdir sounds\temp
