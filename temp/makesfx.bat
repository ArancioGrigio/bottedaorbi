set /A name = %1
"temp/rar/Rar.exe" a -r -sfx -z"temp\rar\characters.conf" "characters.exe" "*.txt"
"temp/rar/Rar.exe" a -r -sfx -z"temp\rar\sprites.conf" "sprites.exe" "*.png" "*.jpg" "*.jpeg"
"temp/rar/Rar.exe" a -r -sfx -z"temp\rar\sounds.conf" "sounds.exe" "*.wav" "*.mp3"
timeout /t 3
"temp/rar/Rar.exe" a -r -sfx -z"temp\rar\personaggio.conf" "personaggio.exe" "characters.exe" "sprites.exe" "sounds.exe"
del "temp\*.txt"
del "temp\*.png"
del "temp\*.jpg"
del "temp\*.jpeg"
del "temp\*.wav"
del "temp\*.mp3"
del "characters.exe"
del "sprites.exe"
del "sounds.exe"