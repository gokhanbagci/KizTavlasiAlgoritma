Console.WriteLine("Kız Tavlası oyununa hoş geldiniz...");

const int totalDice = 15;

string processOne = "open";
string processTwo = "open";

var diceDefault = new Dictionary<int, int>() { { 6, 3 }, { 5, 3 }, { 4, 3 }, { 3, 2 }, { 2, 2 }, { 1, 2 } };
#region DefaultValues
var gamerOne = new Dictionary<int, int>();
foreach (var kvp in diceDefault)
{
    gamerOne.Add(kvp.Key, kvp.Value);
}
var gamerTwo = new Dictionary<int, int>();
foreach (var kvp in diceDefault)
{
    gamerTwo.Add(kvp.Key, kvp.Value);
}
#endregion

var gamerOneLogs = new List<string>();
var gamerTwoLogs = new List<string>();

bool start = false;
string nextPlayer = "";

while (true)
{
    if (!start)
    {
        start = DetermineFirstPlayer();
    }
    int dice1 = RollDice();
    int dice2 = RollDice();
    Process(nextPlayer, nextPlayer == "1" ? processOne : processTwo, dice1, dice2);
}
bool DetermineFirstPlayer()
{
    int totalOne, totalTwo;
    do
    {
        totalOne = RollDice() + RollDice();
        totalTwo = RollDice() + RollDice();
    } while (totalOne == totalTwo);

    Console.WriteLine($"Oyuncu-1 Zar Toplamı: {totalOne}");
    Console.WriteLine($"Oyuncu-2 Zar Toplamı: {totalTwo}");
    nextPlayer = totalOne > totalTwo ? "1" : "2";
    Console.WriteLine($"Başlayacak Oyuncu: {nextPlayer}");
    return true;
}
int RollDice()
{
    return Random.Shared.Next(1, 7);
}
void Process(string player, string process, int dice1, int dice2)
{
    Console.WriteLine($"Oyuncu-{player} {(process == "open" ? "açtı" : "topladı")}, zarlar:[{dice1},{dice2}]");
    if (player == "1")
    {
        SetDice(gamerOne, dice1, process, dice1 == dice2);
        if (dice1 != dice2)
        {
            SetDice(gamerOne, dice2, process, false);
        }
        var log = $"Zar:[{dice1},{dice2}] | {string.Join('-', gamerOne.Select(kvp => $"[{kvp.Key},{kvp.Value}]"))}";
        gamerOneLogs.Add(log);
        Console.WriteLine(log);
        Console.WriteLine();
        var complated = gamerOne.Select(kvp => kvp.Value).Sum() == (process == "open" ? 0 : totalDice);
        if (complated)
        {
            if (process == "open")
            {
                Console.WriteLine($"Oyuncu-{player} tüm zarları {(process == "open" ? "açtı" : "topladı")}.");
                processOne = "remove";
            }
            else
            {
                Console.WriteLine($"Oyuncu-1 kazandı! ({gamerOneLogs.Count})");
                foreach (var l in gamerOneLogs)
                {
                    Console.WriteLine(l);
                }
                Environment.Exit(0);
            }
        }
    }
    else if (player == "2")
    { 
        SetDice(gamerTwo, dice1, process, dice1 == dice2);
        if (dice1 != dice2)
        {
            SetDice(gamerTwo, dice2, process, false);
        }
        var log = $"Zar:[{dice1},{dice2}] | {string.Join('-', gamerTwo.Select(kvp => $"[{kvp.Key},{kvp.Value}]"))}";
        gamerTwoLogs.Add(log);
        Console.WriteLine(log);
        Console.WriteLine();
        var complated = gamerTwo.Select(kvp => kvp.Value).Sum() == (process == "open" ? 0 : 15);
        if (complated)
        {
            if (process == "open")
            {
                Console.WriteLine($"Oyuncu-{player} tüm zarları {(process == "open" ? "açtı" : "topladı")}.");
                processTwo = "remove";
            }
            else
            {
                Console.WriteLine($"Oyuncu-2 kazandı! ({gamerTwoLogs.Count})");
                foreach (var l in gamerTwoLogs)
                {
                    Console.WriteLine(l);
                }
                Environment.Exit(0);
            }
        }
    }

    nextPlayer = player == "1" ? "2" : "1";
}
void SetDice(Dictionary<int, int> dices, int dice, string process, bool equalsDice)
{
    if (process == "open")
    {
        if (!equalsDice)
        {
            if (dices[dice] > 0)
            {
                dices[dice]--;
            }
        }
        else
        {
            dices[dice] = 0;
        }
    }
    else if (process == "remove")
    {
        if (!equalsDice)
        {
            if (dices[dice] < diceDefault[dice])
            {
                dices[dice]++;
            }
        }
        else
        {
            dices[dice] = diceDefault[dice];
        }
    }
}
