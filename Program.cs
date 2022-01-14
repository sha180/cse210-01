/*
    tic-tac-toe
    by Dallin Shaeffer
    cse 210-01
*/
using System;

namespace TicTacToe
{
    class Program
    {

        static void Main(){

            int boardSize = getGameBoardSize();
            
            string[,] boardLable = new string[boardSize,boardSize];
            char[,] playerBoard = new char[boardSize,boardSize];

            initGame(boardLable, playerBoard, boardSize);
            gameLoop(boardLable, playerBoard, boardSize);

        }

        static void gameLoop(string[,] boardLable, char[,] playerBoard, int boardSize){
            
            bool gameGoing = false;
            int playersTurn = 0;
            int chosenLable;
            char player;
            while(!gameGoing){

                Console.WriteLine($" players turn = {playersTurn}");
                if (isEven(playersTurn)){
                    player = 'X';
                }else{
                    player = 'O';
                }

                chosenLable = editPlayerBoaed(boardLable, playerBoard, boardSize, player);
                // Console.WriteLine("size = " + boardSize);
                if (chosenLable >= 1){
                    gameGoing = checkBoard(chosenLable, playerBoard, boardSize, player, boardSize);
                    playersTurn++;
                }
                if(playersTurn == boardSize * boardSize){
                    displayBoard(boardLable, playerBoard, boardSize);
                    Console.WriteLine("its a Tie, better luck next time");
                    break;
                }
                if (gameGoing){
                    displayBoard(boardLable, playerBoard, boardSize);
                    Console.Write($"Good game. player ");
                     if (player == 'X'){ 
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }else{
                        Console.ForegroundColor = ConsoleColor.Green;
                    } 
                    Console.Write($"{player}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" won! Thanks for playing!");
                }
                    
            }
        }

        static int editPlayerBoaed(string[,] boardLable, char[,] playerBoard, int boardSize, char player){
            
            displayBoard(boardLable, playerBoard, boardSize);

            int maxSize = boardSize * boardSize;
         
            if (player == 'X'){ 
                Console.ForegroundColor = ConsoleColor.Blue;
            }else{
                Console.ForegroundColor = ConsoleColor.Green;
            } 
            
            Console.Write($"{player}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"'s turn to choose a square (1-{maxSize}): ");

            int chosenLable = getIntFromCMD();
            if (chosenLable < 0 ){
                
                return -1;
            }
            if (chosenLable > maxSize){
                Console.WriteLine(chosenLable + $" is to large enter a number (1-{maxSize})");
                return - 1;
            }
            int posX = (chosenLable - 1)%boardSize;

            int posY = (chosenLable%boardSize == 0 ? chosenLable - 1: chosenLable)/boardSize;
            
            if (playerBoard[posX,posY] != '\0'){
                Console.WriteLine("that space is already taken");
                return -1;
            }else {
                playerBoard[posX,posY] = player;
            }
            return chosenLable;

        }

        // set up the game veriable and 
        // player chericters
        static int initGame(string[,] boardLable, char[,] playerBoard, int boardSize){
            setupBoardLables(boardLable, boardSize);
            
            return 0;
        }


        static bool checkBoard(int chosenLable, char[,] playerBoard, int boardSize, char player, int AmountToWin ){

            
            int[,] pos = new int[boardSize,boardSize];

            int[] posY = new int[boardSize*boardSize];
            int[] posX = new int[boardSize*boardSize];

            int loop = 0;

            // looks for all the places a player has set there mark
            for (int i = 0; i < boardSize; i++){
                for (int j = 0; j < boardSize; j++){

                    char tmpChar = playerBoard[j, i];
                    // Console.WriteLine($"tmpChar = {tmpChar}");
                    if (tmpChar == player){
                        posY[loop] = i;
                        posX[loop] = j;
                        pos[j,i] = player;

                    // Console.WriteLine($"j = {j}, i = {i}");
                    // Console.WriteLine($"tmpChar = {tmpChar}");
                        loop++;
                    }
                }
            }
            Console.WriteLine();

            if (loop > 1){
                
                if (veritcalSearch2(playerBoard, boardSize, AmountToWin, player)) return true;
                if (horzontalSearch2(playerBoard, boardSize, AmountToWin, player)) return true;
                if (desendingSearch2(playerBoard, boardSize, AmountToWin, player)) return true;
                if (asindingSearch2(playerBoard, boardSize, AmountToWin, player)) return true;

                // if (veritcalSearch(posY, posX, boardSize, AmountToWin)) return true;
                // if (horzontalSearch(posY, posX, boardSize, AmountToWin)) return true;
                // if (desendingSearch(posY, posX, boardSize, AmountToWin)) return true;
                // if (asindingSearch(posY, posX, boardSize, AmountToWin)) return true;
            }
            return false;
        }

        static bool veritcalSearch2(char[,] pos, int boardSize, int AmountToWin , char player){

            int amountInRow = 0;
            int[] tmp = new int[boardSize];

            for (int i = 0; i < boardSize; i++){
                for (int j = 0; j < boardSize; j++){
                    if(pos[i,j] == player){
                        amountInRow++;
                    }
                    if (amountInRow == AmountToWin){
                        amountInRow ++;
                        return true;
                    }
                }
                amountInRow = 0;
            }

            return false;
        }

        static bool horzontalSearch2(char[,] pos, int boardSize, int AmountToWin , char player){

            int amountInRow = 0;
            int[] tmp = new int[boardSize];

            for (int i = 0; i < boardSize; i++){
                for (int j = 0; j < boardSize; j++){
                    if(pos[j,i] == player){
                        amountInRow++;
                    }
                    if (amountInRow == AmountToWin){
                        amountInRow ++;
                        return true;
                    }
                }
                amountInRow = 0;
            }

            return false;
        }

        static bool desendingSearch2( char[,] pos, int boardSize, int AmountToWin, char player){
            
            int amountInRow = 0;
            int k = 0;
            for (int i = 0; i < boardSize; i ++){
                for (int j = 0; j < boardSize;){
                    

                        // Console.WriteLine("diss " + j );
                    if (pos[i,j] == player){
                        if(i == j){
                        amountInRow ++;
                        // Console.WriteLine("diss " + amountInRow);
                    }}
                    if (amountInRow == AmountToWin){
                        amountInRow ++;
                        // Console.WriteLine("diss win" + amountInRow);
                        return true;
                    }
                    k++;
                    j++;
                }
                k = 0;
            } 
            return false;
        }

        
        static bool asindingSearch2(char[,] pos, int boardSize, int AmountToWin, char player){

            int amountInRow = 0;

            for (int i = boardSize - 1; i > -1; ){
                for(int j = 0; j < boardSize; ){
                    if(pos[j,i] == player){
                        amountInRow ++;
                        Console.WriteLine("ass " + player);
                    }
                    
                    if (amountInRow == AmountToWin) {
                        Console.WriteLine("ass win" + amountInRow);
                        amountInRow ++;
                        return true;
                    }
                    i--;
                    j++;
                }
            }
            return false;
        }

// search the cordinets of the players tokens to see of they line up verticly
        static bool veritcalSearch(int[] posY, int[] posX, int boardSize, int AmountToWin){

            int amountInRow = 0;

            for (int i = 0; i < boardSize * boardSize - 1; i++){

                if(posX[i] == posX[i+1] && posY[i] + 1 == posY[i+1]){
                    amountInRow++;
                    Console.WriteLine("amount in row " + amountInRow);
                }
                if (amountInRow == AmountToWin - 1){
                    amountInRow ++;
                    return true;
                }
            }

            return false;
        }


// search the cordinets of the players tokens to see of they line up horizontaly
        static bool horzontalSearch(int[] posY, int[] posX, int boardSize, int AmountToWin){

            int amountInRow = 0;

            for (int i = 0; i < boardSize * boardSize - 1; i ++){
                if(posX[i] + 1 == posX[i+1] && posY[i] == posY[i+1]){
                    amountInRow ++;
                }
                if(amountInRow == AmountToWin - 1){
                    amountInRow ++;
                    return true;
                }
            }
            return false;
        }


// search the cordinets of the players tokens to see of they line from the top corner to the bottem
        static bool desendingSearch(int[] posY, int[] posX, int boardSize, int AmountToWin){
            
            int amountInRow = 0;

            for (int i = 0; i < boardSize * boardSize - 1; i++){
                if (posX[i] + 1 == posX[i+1] && posY[i] + 1 == posY[i+1]){
                    amountInRow ++;
                }
                if (amountInRow == AmountToWin - 1){
                    amountInRow ++;
                    return true;
                }
            } 
            return false;
        }

// search the cordinets of the players tokens to see of they line from the bottem corner to the top
        static bool asindingSearch(int[] posY, int[] posX, int boardSize, int AmountToWin){

            int amountInRow = 0;

            for (int i = 0; i < boardSize * boardSize - 1; i++){
                if(posX[i] - 1 == posX[i+1] && posY[i] + 1 == posY[i+1]){
                    amountInRow ++;
                }
                if (amountInRow == AmountToWin - 1) {
                    amountInRow ++;
                    return true;
                }
            }
            return false;
        }

        // gets and sets the boardlable numbers
        // and sets the board size;
        static int getGameBoardSize(){

            Console.WriteLine("What board size do you want?");

            int boardSize = getIntFromCMD();

            int loop = 0;

            while(boardSize < 3){
                Console.WriteLine("Enter a number grater than 3");
                loop++;

                if (loop == 4){
                    Console.WriteLine("failed to get board size. aborting application!");
                    boardSize = -1;
                    break;
                }
                boardSize = getIntFromCMD();

            }

            return boardSize;

        }

        // gives lables to all the squares on the tic tac toe board
        static void setupBoardLables(string[,] boardLables, int boardSize){
            int maxNumber = boardSize * boardSize;

            int lableSpacing = maxNumber.ToString().Length;

            int lablesNumber = 0;
            int lableLength;
            for (int i = 0; i < boardSize; i++){
                for (int j = 0; j <boardSize; j++){
                    lablesNumber += 1;

                    lableLength = lablesNumber.ToString().Length - lableSpacing;

                    boardLables[j,i] = lablesNumber.ToString();

                    if(lableLength < 1){
                        for (int k = -1*lableLength; k > 0; k-- ){
                            boardLables[j,i] += " ";
                        }
                    }
                }
            }

        }

        static void displayBoard(string[,] boardLable, char[,] playerBoard, int boardSize){
            int maxNumber = boardSize * boardSize;

            int spacing = maxNumber.ToString().Length;


            // print the X's and O's where they should go 
            // leaving nummbers where a player has not 
            // set an 'X' or 'O'
            for (int i = 0; i < boardSize; i++){
                for (int j = 0; j < boardSize; j++){
                    
                    Console.ForegroundColor = ConsoleColor.White;
                    if (j !=0) Console.Write("|");
                    string write;

                    if(playerBoard[j,i] == '\0'){
   

                        write = boardLable[j,i];
                    }else{
                        if (playerBoard[j,i] == 'X'){ 
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }else{
                            Console.ForegroundColor = ConsoleColor.Green;
                        } 
                        write = playerBoard[j,i].ToString();
                        if(spacing > 1){
                            for (int k = 0; k < spacing - 1; k++){
                                write += " ";
                            }
                        }

                    }
                    Console.Write($"{write}");
                }

                    Console.ForegroundColor = ConsoleColor.White;
                // prints out the row deviders
                Console.WriteLine();
                if(i <= boardSize - 2){
                    for (int x = 0; x < (boardSize*2)- 1; x++){
                        if (isEven(x)){
                            for (int y = 0; y < spacing; y++){
                                Console.Write("-");
                            }
                        }else{
                            Console.Write("+");
                        }

                    }
                }
                Console.WriteLine();
            }
        }

        // retreves an intiger from user input
        static int getIntFromCMD(){

            string input = Console.ReadLine();


            if (!int.TryParse(input, out int num)){
                if (input != null)
                input = input.ToLower();


                Console.WriteLine( input + " is not a number!");
                return -1;
            }else{
                return num;
            }
        }

        // a function that returns true if an int is even
        static bool isEven(int i){
            
            bool even = i%2 == 0 ? true : false;
            
            return even;
        }
    }
}
