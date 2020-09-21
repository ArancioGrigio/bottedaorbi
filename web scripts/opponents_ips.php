<?
	$servername = "localhost";									//Definizione dati di accesso al satabase mySQL
	$username = "pippotaxi";
	$passworda = "";
	$dbname = "my_pippotaxi";
	$conn = new mysqli($servername, $username, $passworda, $dbname);			//Connessione al database mySQL

$time = time() - 30;	//Il ping avviene ogni 10 secondi, per sicurezza metto 30
$sql = "SELECT ip FROM Bottedaorbi WHERE last_ping > ".$time;
$result = $conn->query($sql);
if ($result->num_rows > 0) {
	while($row = $result->fetch_assoc()) {
		echo $row['ip'].",";
	}
}

$conn->close();
?>