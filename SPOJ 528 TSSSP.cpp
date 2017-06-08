// Made by Mariusz Witkowski 
// This program finds the shortest superstring
// that contains all given strings as substrings

#include <cstdlib>
#include <iostream>

void test(int k);
void buildDirectedGraph(int graph[100][100], int graphSize, std::string stringTab[], int &maxOverlap, int &i_idx_max, int &j_idx_max);
void mergeStrings(std::string TempStringArr[], int maxOverlap, int i_max, int j_max, int &rememberLast);
void refreshGraph(int graph[100][100], int &graphSize, std::string stringTab[], int &maxOverlap, int &i_idx_max, int &j_idx_max, int rememberLast);
int findOverlap(std::string str1, std::string str2);

void test(int k)
{
    int numberOfStrings;

    scanf ("%d", &numberOfStrings);

    std::string PrimaryTab[numberOfStrings];
    std::string TempStringArr[numberOfStrings];

    for(int i = 0; i < numberOfStrings; i++)
    {
        std::cin >> PrimaryTab[i];
        TempStringArr[i] = PrimaryTab[i];
    }
     std::cout << "case " << k << " Y" << "\n";

    int counter = numberOfStrings;
    int counter2 = numberOfStrings;
    int DirectedGraph[100][100];
    int maxOverlap = 0;
    int i_idx_max = 0;
    int j_idx_max = 0;
    int rememberLast = 0;
   
    buildDirectedGraph(DirectedGraph, numberOfStrings, TempStringArr, maxOverlap, i_idx_max, j_idx_max);
     
    while (counter > 1)
    {
        mergeStrings(TempStringArr, maxOverlap, i_idx_max, j_idx_max, rememberLast);
        refreshGraph(DirectedGraph, numberOfStrings, TempStringArr, maxOverlap, i_idx_max, j_idx_max, rememberLast);
        counter--;
   }
    std::cout << TempStringArr[rememberLast] << "\n";
    
    for (int i = 0, posOfFirstLetter=0; i < counter2; i++)
    {
        posOfFirstLetter = TempStringArr[rememberLast].find(PrimaryTab[i])+1;
        std::cout << posOfFirstLetter << "\n";
    }

}

void buildDirectedGraph(int graph[100][100], int graphSize, std::string stringTab[], int &maxOverlap, int &i_idx_max, int &j_idx_max)
{
    for( int i = 0; i < graphSize; i++)
    {
        for (int j = 0; j < graphSize; j++)
        {
            if ( i!=j)
            {
                graph[i][j] = findOverlap (stringTab[i], stringTab[j]);
                if (graph[i][j] >= maxOverlap)
                {
                    maxOverlap = graph[i][j];
                    i_idx_max = i;
                    j_idx_max = j;
                }
            }else
                graph[i][j] =-1;
        }
    }
}

void mergeStrings(std::string TempStringArr[], int maxOverlap, int i_max, int j_max, int &rememberLast)
{	
	if (j_max > i_max)
	{
	    TempStringArr[i_max] = TempStringArr[i_max] + TempStringArr[j_max].substr(maxOverlap, TempStringArr[j_max].length());
	    TempStringArr[j_max] = "";
	    rememberLast = i_max;
	}else
	{
		TempStringArr[j_max] = TempStringArr[i_max] + TempStringArr[j_max].substr(maxOverlap, TempStringArr[j_max].length());
	    TempStringArr[i_max] = "";
	    rememberLast = j_max;
	}
}

void refreshGraph(int graph[100][100], int &graphSize, std::string stringTab[], int &maxOverlap, int &i_idx_max, int &j_idx_max, int rememberLast)
{
    int old_i_max = i_idx_max;
    int old_j_max = j_idx_max;
    
    int lineToZero = std::max(j_idx_max,i_idx_max);
    
    if ( lineToZero == graphSize -1)
        graphSize--;

    maxOverlap = 0;
    i_idx_max = 0;
    j_idx_max = 0;
    
	for (int i =0; i < graphSize; i++)
    {
        if (stringTab[i]!="" and i!=rememberLast)
        {
            graph[rememberLast][i] = findOverlap (stringTab[rememberLast], stringTab[i]);
            graph[i][rememberLast] = findOverlap (stringTab[i], stringTab[rememberLast]);
        }
        graph[i][lineToZero] = -1;
        graph[lineToZero][i] = -1;
    }
	
    for (int i = 0; i < graphSize; i++)
    {
        for(int j = 0; j < graphSize; j++)
        {
            if (i!=j)
            {
                if(graph[i][j] >= maxOverlap)
                {
                    maxOverlap = graph[i][j];
                        i_idx_max = i;
                        j_idx_max = j;
                }
            }
        }
    }
}

int findOverlap(std::string str1, std::string str2)
{
    int lenStr1 = str1.length();
    int lenStr2 = str2.length();
    int numOfIter = std::min(lenStr1, lenStr2);
    int Overlap = 0;

    for (int i = 1; i <= numOfIter; i++)
    {
        if (str1.compare((lenStr1-i), i, str2, 0, i) == 0)
            Overlap = i;
    }

    return Overlap;
}

int main()
{
    int testCases;
    std::cin >> (testCases);

    for (int i =1 ; i <= testCases; i++)
    {
        test(i);
    }
    return 0;
}
