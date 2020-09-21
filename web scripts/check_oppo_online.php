<?php
$servername = "localhost";									//Definizione dati di accesso al satabase mySQL
$username = "pippotaxi";
$passworda = "";
$dbname = "my_pippotaxi";
$conn = new mysqli($servername, $username, $passworda, $dbname);			//Connessione al database mySQL

$oppo = $_GET["oppo"];
$sql = "SELECT last_ping FROM Bottedaorbi WHERE ip = '".$oppo."'";
$result = $conn->query($sql);
$row = $result->fetch_assoc();
echo $row['last_ping'];

$conn->close();
?>