string [,] ourPlayBoard = new string[15,15];
int point=0;
int position = ourPlayBoard.GetLength(1)/2;
Console.Clear();
Console.WriteLine("Привет! В этой игре тебе необходимо заполнять первый ряд, стреляя блоками. ");
Console.WriteLine("Каждый твой ход на поле в случайном месте первого ряда будет появляться несколько блоков, смещая весь ряд вниз.");
Console.WriteLine("Количество блоков зависит от выбранной тобой сложности");
Console.WriteLine("Если блоки достигнут твоего персонажа - Ты проиграл.");
Console.WriteLine("Если заполнить первый ряд целиком, он исчезнет, а оставшиеся ряды сместятся вверх.");
Console.WriteLine("Для управления используй свою клавиатуру:");
Console.WriteLine("'w' - выстрелить блоком");
Console.WriteLine("'a' - передвинуть персонажа влево");
Console.WriteLine("'d' - передвинуть персонажа вправо");
Console.WriteLine("'x' - сдаться и досрочно завершить игру");
Console.WriteLine();
Console.WriteLine("Выбери сложность вписав соответствующую цифру:");
Console.WriteLine("1 - я только попробовать");
Console.WriteLine("2 - готов к труду и обороне");
Console.WriteLine("3 - все карты на стол");
Console.WriteLine("4 - немного камикадзе");
int hardLvL = Convert.ToInt32(Console.ReadLine());
if (hardLvL > 4)
{
    Console.WriteLine("Я же говорю, уровня всего 4...");
    Console.WriteLine("Ну чтож... это твое решение ;(");
    hardLvL = 10;
}
else
{
    Console.WriteLine("Отлично! Ну что, начнем?");
}
Console.WriteLine("Нажми Enter, когда будешь готов");
Console.ReadLine();
ourPlayBoard = CreateBoard(ourPlayBoard);
ourPlayBoard = MakeHero(ourPlayBoard, position);
PrintPlayingField(ourPlayBoard);
while (true)
{
    
    MoveOrFire();
    point+=CountPoint(ourPlayBoard);
    ClearRow(ourPlayBoard);
    Console.WriteLine($"Ты заработал {point} {wordPoint(point)}");
    for (int i = 0; i < hardLvL; i++)
    {
        MakeNewRandomCell(ourPlayBoard);
    }
    
    
}
void MakeNewRandomCell (string[,]board) //выбирает случайную х координату и добавляет по ней новый блок, смещая боли того же ряда на 1 вниз
{
    int randPos = new Random().Next(1, board.GetLength(1) - 1);
    if (board[board.GetLength(0) - 4, randPos] == "▬▬▬")
    {
        System.Console.WriteLine("Ты проиграл");
        Environment.Exit(0); 
    }
    for (int y = board.GetLength(0) - 5; y > 0; y--)
    {
        if (board[y,randPos] == "▬▬▬")
        {
            board[y,randPos] = "   ";
            board[y+1,randPos] = "▬▬▬";
        }
    }
    board[1, randPos] = "▬▬▬";
}


void ClearRow(string[,]board) //очищает первый ряд и отрисовывает доску, если ряд заполнен
{
    int count = 0;
    for (int i = 1; i < board.GetLength(1); i++) //считаем количество заполненных ячеек в первой строке поля
    {
        if(board[1,i] == "▬▬▬")
        {
            count++;
        }
    }
    if(count == board.GetLength(1) - 2)  //проверяем выполнение условия для стирания первой строки и стираем
    {
        for (int i = 1; i < board.GetLength(1) - 1; i++)
        {
            board[1, i] = "   ";
        }
        for (int x = 1; x < board.GetLength(1)-1; x++)
        {
            for (int y = 2; y < board.GetLength(0) -2; y++)
            {
                if(board[y,x] == "▬▬▬")
                {
                    board[y,x] = "   ";
                    board[y-1,x] = "▬▬▬";
                }
            }
        }        
    }
    PrintPlayingField(ourPlayBoard);
}


int CountPoint (string[,]board)  //отслеживает заполнение ряда и считает заработанные очки
{
    int tempPoint=0;
    int count = 0;
    for (int i = 1; i < board.GetLength(1); i++) //считаем количество заполненных ячеек в первой строке поля
    {
        if(board[1,i] == "▬▬▬")
        {
            count++;
        }
    }
    if(count == board.GetLength(1) - 2)  //проверяем выполнение условия для стирания первой строки и увеличения колличества очков игрока
    {        
        tempPoint++;
    }
    return tempPoint;
   
}


string wordPoint(int point) //Определяем окончание слова "балл"
{
    string wordPoint = string.Empty;
    if (point %10 == 1)         
    {
        wordPoint = "балл";
    }
    else if (point %10 > 1 && point%10 < 5 && (point < 10 || point > 14))
    {
        wordPoint = "балла";
    }
    else
    {
        wordPoint = "баллов";
    }
    return wordPoint;
}


string [,] CreateBoard (string[,]board)  //принимает массив и отрисовывает текущее состояние игрового полня
{   
    for (int x = 0; x < board.GetLength(1); x++)
    {
        for (int y = 0; y< board.GetLength(0); y++)
        {
            if (y == 0 || y == (board.GetLength(1) -1) || x == 0 || x == board.GetLength(0) -1)
            {
                board[y,x] = " x ";
            }            
            else
            {
                board[x,y] = "   ";
            }
        }
    }    
    return board;
}


void PrintPlayingField (string[,]board)  //отрисовывает доску
{
    Console.Clear();
    for (int x = 0; x < board.GetLength(1); x++)
    {
        for (int y = 0; y < board.GetLength(0); y++)
        {
            Console.Write(board[x,y]);
        }
        Console.WriteLine();
    }
}


string [,] MakeHero (string[,]board, int coordinate) // создает персонажа
{
    board[board.GetLength(0) - 2, coordinate] = " █ ";
    return board;
    
}


string [,] Fire (string[,]board, int coordinate) //заполняет дальнюю не заполненную ячейку "стреляет"
{
    if(board[board.GetLength(0)-4, coordinate] != "▬▬▬")
    {
    for (int y = board.GetLength(0) -4; y >= 0; y--)
    {
        if (board[y, coordinate] == "▬▬▬" || y == 0)
        {
            board[y+1, coordinate] = "▬▬▬";
        }
       
    }
    }
    else
    {
        System.Console.WriteLine("Ты проиграл");
        Environment.Exit(0);
    }
    return board;
}


void MoveOrFire () //совершает действие по выбору игрока
{   
    var button = Console.ReadKey();
    switch(button.KeyChar)
    {
        case 'w': Fire(ourPlayBoard, position);
        PrintPlayingField(ourPlayBoard);
        break;
        case 'a':
        if (position > 1)
        {
            ourPlayBoard[ourPlayBoard.GetLength(0) - 2, position] = "   ";
            position -=1;
            MakeHero(ourPlayBoard, position);
        }
            PrintPlayingField(ourPlayBoard);        
        break;
        case 'd':
        if (position < ourPlayBoard.GetLength(1) -2)
        {
            ourPlayBoard[ourPlayBoard.GetLength(0) - 2, position] = "   ";
            position +=1;
            MakeHero(ourPlayBoard, position);
        }
        PrintPlayingField(ourPlayBoard);
        break;
        case 'x': 
        Environment.Exit(0);
        break;
        default:
        Console.WriteLine("Использованная тобой клавиша не подходит для управления персонажем.");
        Console.WriteLine("Для управления используй свою клавиатуру(маленькие буквы на английской раскладке):");
        Console.WriteLine("'w' - выстрелить блоком");
        Console.WriteLine("'a' - передвинуть персонажа влево");
        Console.WriteLine("'d' - передвинуть персонажа вправо");
        Console.WriteLine("'x' - сдаться и досрочно завершить игру");
        Console.WriteLine("Нажми Enter, когда будешь готов");
        Console.ReadLine();
        MoveOrFire();
        break;
    }
}

  
