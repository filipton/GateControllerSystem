<?php
$secret = $_GET['key'];
if(isset($secret))
{
    $s1 = "http://192.168.1.18:21371/";
    $s2 = "http://192.168.1.18:21372/";
    $s3 = "http://192.168.1.18:21373/";

    $s1Status = exec('curl "'.$s1.'info"');
    $s2Status = exec('curl "'.$s2.'info"');
    $s3Status = exec('curl "'.$s3.'?m=info"');
    if($_GET['m'] == "status")
    {
        echo('Server 1: '.$s1Status.'<br>');
        echo('Server 2: '.$s2Status.'<br>');
        echo('Server 3: '.$s3Status.'<br>');
    }

    if($s1Status == "Ok!")
    {
        echo(exec('curl "'.$s1.'gate/'.$secret.'"'));
    }
    else if($s2Status == "Ok!")
    {
        echo(exec('curl "'.$s2.'gate/'.$secret.'"'));
    }
    else if($s3Status == "Ok!")
    {
        echo(exec('curl "'.$s3.'?sk='.$secret.'"'));
    }
    else
    {
        echo("Żaden serwer nie dziala... Skontaktuj się z administratorem!");
    }
}
?>