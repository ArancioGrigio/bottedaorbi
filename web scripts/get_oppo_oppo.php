<?php
$servername = "localhost";									//Definizione dati di accesso al satabase mySQL
$username = "pippotaxi";
$passworda = "";
$dbname = "my_pippotaxi";
$conn = new mysqli($servername, $username, $passworda, $dbname);			//Connessione al database mySQL

$ip = $_SERVER['REMOTE_ADDR'];
$oppo_ip = $_GET["oppo_ip"];
$sql = "SELECT oppo FROM Bottedaorbi WHERE ip = '".$oppo_ip."'";
$result = $conn->query($sql);
if ($result->num_rows > 0) {
	$row = $result->fetch_assoc();
	echo $row['oppo'];
}

$conn->close();
?>