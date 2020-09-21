<?php
$servername = "localhost";									//Definizione dati di accesso al satabase mySQL
$username = "pippotaxi";
$passworda = "";
$dbname = "my_pippotaxi";
$conn = new mysqli($servername, $username, $passworda, $dbname);			//Connessione al database mySQL

$oppo_ip = $_GET["oppo_ip"];
$sql = "SELECT move, last_move, success FROM Bottedaorbi WHERE ip = '".$oppo_ip."'";
$result = $conn->query($sql);
if ($result->num_rows > 0) {
	$row = $result->fetch_assoc();
	echo $row['move'].",".$row['last_move'].",".$row['success'];
}

$conn->close();
?>