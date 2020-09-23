<?php
$secret_key = $_GET['sk'];
if(isset($secret_key))
{
    $myfile = fopen("/var/secret_key", "r") or die("Unable to open file!");
    $rkey = fread($myfile,filesize("/var/secret_key"));
    fclose($myfile);

    if($secret_key == $rkey)
    {
        echo("Otwieranie bramy... (Server 3)");
        system("gpio -g mode 4 out");
        system("gpio -g write 4 1");
        usleep(600000);
        system("gpio -g write 4 0");
    }
    else
    {
        echo("Zły klucz dostępu... (Server 3)");
    }
}
else
{
    $mode = $_GET['m'];
    if(isset($mode))
    {
        if($mode == "info")
        {
            echo("Ok!");
        }
    }
}
?>