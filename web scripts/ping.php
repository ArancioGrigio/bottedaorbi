<?php
$servername = "localhost";									//Definizione dati di accesso al satabase mySQL
$username = "pippotaxi";
$passworda = "";
$dbname = "my_pippotaxi";
$conn = new mysqli($servername, $username, $passworda, $dbname);			//Connessione al database mySQL

$oppo = $_GET['oppo'];
$character = $_GET['character'];
$time = time();
$ip = $_SERVER['REMOTE_ADDR'];
$sql = "SELECT last_ping FROM Bottedaorbi WHERE ip = '".$ip."'";
$result = $conn->query($sql);
if ($result->num_rows > 0)
	$sql = "UPDATE Bottedaorbi SET last_ping = '".$time."', oppo = '".$oppo."', _character = '".$character."' WHERE ip = '".$ip."'";
else
	$sql = "INSERT INTO Bottedaorbi (ip, move, oppo, _character, last_ping, last_move) VALUES ('$ip', 0, '".$oppo."', '".$character."', $time, 0)";

$result = $conn->query($sql);
$conn->close();
?>