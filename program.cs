Console.WriteLine("Kız Tavlası oyununa hoş geldiniz...");

const int totalStamp = 15;

ProcessType processOne = ProcessType.Open;
ProcessType processTwo = ProcessType.Open;

var diceDefault = new Dictionary<int, int>() { { 6, 3 }, { 5, 3 }, { 4, 3 }, { 3, 2 }, { 2, 2 }, { 1, 2 } };
var gamerOneDices = diceDefault.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
var gamerTwoDices = diceDefault.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

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
void Process(string player, ProcessType processType, int dice1, int dice2)
{
    Console.WriteLine($"Oyuncu-{player} {(processType == ProcessType.Open ? "açtı" : "topladı")}, zarlar:[{dice1},{dice2}]");
    if (player == "1")
    {
        SetDice(gamerOneDices, dice1, processType, dice1 == dice2);
        if (dice1 != dice2)
        {
            SetDice(gamerOneDices, dice2, processType, false);
        }
        var complated = gamerOneDices.Select(kvp => kvp.Value).Sum() == (processType == ProcessType.Open ? 0 : totalStamp);
        var log = $"Zar:[{dice1},{dice2}] {string.Join('-', gamerOneDices.Select(kvp => $"[{kvp.Key},{kvp.Value}]"))} - {complated}";
        gamerOneLogs.Add(log);
        Console.WriteLine(log);
        Console.WriteLine();
        if (complated)
        {
            if (processType == ProcessType.Open)
            {
                Console.WriteLine($"Oyuncu-{player} tüm zarları {(processType == ProcessType.Open ? "açtı" : "topladı")}.");
                processOne = ProcessType.Remove;
            }
            else
            {
                Console.WriteLine($"Oyuncu-1 kazandı! ({gamerOneLogs.Count})");
                for (int i = 0; i < gamerOneLogs.Count; i++)
                {
                    string? l = gamerOneLogs[i];
                    Console.WriteLine($"[{i.ToString().PadLeft(2, '0')}] {l}");
                }
                Environment.Exit(0);
            }
        }
    }
    else if (player == "2")
    {
        SetDice(gamerTwoDices, dice1, processType, dice1 == dice2);
        if (dice1 != dice2)
        {
            SetDice(gamerTwoDices, dice2, processType, false);
        }
        var complated = gamerTwoDices.Select(kvp => kvp.Value).Sum() == (processType == ProcessType.Open ? 0 : 15);
        var log = $"Zar:[{dice1},{dice2}] {string.Join('-', gamerTwoDices.Select(kvp => $"[{kvp.Key},{kvp.Value}]"))} - {complated}";
        gamerTwoLogs.Add(log);
        Console.WriteLine(log);
        Console.WriteLine();
        if (complated)
        {
            if (processType == ProcessType.Open)
            {
                Console.WriteLine($"Oyuncu-{player} tüm zarları {(processType == ProcessType.Open ? "açtı" : "topladı")}.");
                processTwo = ProcessType.Remove;
            }
            else
            {
                Console.WriteLine($"Oyuncu-2 kazandı! ({gamerTwoLogs.Count})");
                for (int i = 0; i < gamerTwoLogs.Count; i++)
                {
                    string? l = gamerTwoLogs[i];
                    Console.WriteLine($"[{i.ToString().PadLeft(2, '0')}] {l}");
                }
                Environment.Exit(0);
            }
        }
    }
    nextPlayer = player == "1" ? "2" : "1";
}
void SetDice(Dictionary<int, int> dices, int dice, ProcessType processType, bool equalsDice)
{
    switch (processType)
    {
        case ProcessType.Open:
            if (!equalsDice)
            {
                if (dices[dice] > 0)
                {
                    dices[dice] = dices[dice] - 1;
                }
            }
            else
            {
                dices[dice] = 0;
            }
            break;
        case ProcessType.Remove:
            if (!equalsDice)
            {
                if (dices[dice] < diceDefault[dice])
                {
                    dices[dice] = dices[dice] + 1;
                }
            }
            else
            {
                dices[dice] = diceDefault[dice];
            }
            break;
    }
}
enum ProcessType
{
    Open,
    Remove
}
