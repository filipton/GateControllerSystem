<?php
$secret = $_GET['key'];
if(isset($secret))
{
    $s1 = "curl --max-time 0.5 '-HX-Token:NiktNieMozeTegoUkrasc' http://172.104.158.181:21371/";
    $s2 = "curl --max-time 0.5 '-HX-Token:NiktNieMozeTegoUkrasc' http://172.104.158.181:21372/";
    $s3 = "curl --max-time 1 '-HX-Token:NiktNieMozeTegoUkrasc' http://172.104.158.181:21373/";

    $s1Status = exec($s1.'info');
    $s2Status = exec($s2.'info');
    $s3Status = exec($s3.'?m=info');
    if($_GET['m'] == "status")
    {
        echo('Server 1: '.$s1Status.'<br>');
        echo('Server 2: '.$s2Status.'<br>');
        echo('Server 3: '.$s3Status.'<br>');
    }

    if($s1Status == "Ok!")
    {
        echo(exec($s1.'gate/'.$secret));
    }
    else if($s2Status == "Ok!")
    {
        echo(exec($s2.'gate/'.$secret));
    }
    else if($s3Status == "Ok!")
    {
        echo(exec($s3.'?sk='.$secret));
    }
    else
    {
        echo("Żaden serwer nie dziala... Skontaktuj się z administratorem!");
    }
}
?>