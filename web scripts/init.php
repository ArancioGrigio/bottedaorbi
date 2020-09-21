<?php
$servername = "localhost";									//Definizione dati di accesso al satabase mySQL
$username = "pippotaxi";
$passworda = "";
$dbname = "my_pippotaxi";
$conn = new mysqli($servername, $username, $passworda, $dbname);			//Connessione al database mySQL

$ip = $_SERVER['REMOTE_ADDR'];

//Imposta move = 0 ed oppo = "" sull'sql (inizio nuovo game)
$sql = "UPDATE Bottedaorbi SET move = 0, oppo = '' WHERE ip = '".$ip."'";
$result = $conn->query($sql);

$conn->close();

//Stampa l'IP
echo $_SERVER['REMOTE_ADDR'];
?>