<?php
$servername = "localhost";									//Definizione dati di accesso al satabase mySQL
$username = "pippotaxi";
$passworda = "";
$dbname = "my_pippotaxi";
$conn = new mysqli($servername, $username, $passworda, $dbname);			//Connessione al database mySQL

$n_move = $_GET['n_move'];
$success = $_GET['success'];
$time = time();
$ip = $_SERVER['REMOTE_ADDR'];
$sql = "UPDATE Bottedaorbi SET last_move = '".$time."', move = '".$n_move."', success = '".$success."' WHERE ip = '".$ip."'";
$result = $conn->query($sql);

$conn->close();
?>