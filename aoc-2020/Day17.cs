using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day17 : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            var input = Input.AsCharMatrix();
            var cube = InitilaizeSpace(input); 
            var neighbourCoords = InitilaizeNeighbours();

            int size = cube.GetLength(0);

            for(int i = 0; i < 6; i++)
            {
                Printer.Print(cube);

                var newCube = new char[cube.GetLength(0), cube.GetLength(1), cube.GetLength(2)];
                Array.Copy(cube, newCube, newCube.Length);

                for(int z = 0; z < size; z++)
                {  
                    for(int y = 0; y < size; y++)
                    {
                        for(int x = 0; x < size; x++)
                        {
                            var curr = cube[z,y,x];
                            var activeCount = GenerateNeighbours((z,y,x), size, neighbourCoords).Count(x=> cube[x.z, x.y, x.x] == '#');
                            
                            char newState = '.';

                            if(curr == '#')
                                newState = (activeCount == 2 || activeCount == 3) ? '#' : '.';
                            else
                                newState = activeCount == 3 ? '#' : '.';
                            
                            newCube[z,y,x] = newState;
                        }
                    }    
                }

                cube = newCube;
            }

            int count = 0;

            foreach(var (z,y,x) in LoopCube(cube))
                if(cube[z,y,x] == '#')
                    count++;
            
     
            return count.ToString();
        }

        private IEnumerable<(int z, int y, int x)> LoopCube(char[,,] cube)
        {
            int size = cube.GetLength(0);
            for(int z = 0; z < size; z++)
                for(int y = 0; y < size; y++)
                    for(int x = 0; x < size; x++)
                        yield return (z,y,x);
        }

        private IEnumerable<(int w, int z, int y, int x)> Loop4D(char[,,,] cube)
        {
            int size = cube.GetLength(0);
            for(int w = 0; w < size; w++)
                for(int z = 0; z < size; z++)
                    for(int y = 0; y < size; y++)
                        for(int x = 0; x < size; x++)
                            yield return (w,z,y,x);
        }

        private IEnumerable<(int z, int y, int x)> GenerateNeighbours((int z, int y, int x) pos, int size,  List<(int z, int y, int x)> neighbourCoords)
        {
            foreach(var neigh in neighbourCoords)
            {
                if (pos.y + neigh.y < 0 || pos.y + neigh.y >= size
                        || pos.x + neigh.x < 0 || pos.x + neigh.x >= size
                        || pos.z + neigh.z < 0 || pos.z + neigh.z >= size)
                        continue;

                yield return (pos.z + neigh.z, pos.y + neigh.y, pos.x + neigh.x);
            }
        }

         private IEnumerable<(int w, int z, int y, int x)> GenerateNeighbours4D((int w, int z, int y, int x) pos, int size, List<(int w, int z, int y, int x)> neighbourCoords)
        {
            foreach(var neigh in neighbourCoords)
            {
                if (pos.y + neigh.y < 0 || pos.y + neigh.y >= size
                        || pos.x + neigh.x < 0 || pos.x + neigh.x >= size
                        || pos.w + neigh.w < 0 || pos.w + neigh.w >= size
                        || pos.z + neigh.z < 0 || pos.z + neigh.z >= size)
                        continue;

                yield return (pos.w + neigh.w, pos.z + neigh.z, pos.y + neigh.y, pos.x + neigh.x);
            }
        }

        private List<(int z, int y, int x)> InitilaizeNeighbours()
        {
            var n = new List<(int z, int y, int x)>();

            for(int z = -1; z <= 1; z++)
                for(int y = -1; y <= 1; y++)
                    for(int x = -1; x <= 1; x++)
                        if(!(z == 0 && y == 0 && x == 0))
                            n.Add((z, y ,x));
            
            return n;               
        }

        private List<(int w, int z, int y, int x)> InitilaizeNeighbours4D()
        {
            var n = new List<(int w, int z, int y, int x)>();
            for(int w = -1; w <= 1; w++)
                for(int z = -1; z <= 1; z++)
                    for(int y = -1; y <= 1; y++)
                        for(int x = -1; x <= 1; x++)
                            if(!(w==0 && z == 0 && y == 0 && x == 0))
                                n.Add((w, z, y ,x));
            
            return n;               
        }

        private char[,,] InitilaizeSpace(char[,] input)
        {
            int size = input.GetLength(0) + 15;
            int baseX, baseY, baseZ;

            baseX = baseY = baseZ = (size / 2);
            var cube = new char[size,size,size];
            for(int z = 0; z < size; z++)
                for(int y = 0; y < size; y++)
                    for(int x = 0; x < size; x++)
                        cube[z,y,x] = '.';
            
            for(int y = -4; y < 4; y++)
                for(int x = -4; x < 4; x++)
                    cube[baseZ, baseY + y, baseX + x] = input[y + 4, x + 4];
            
            return cube;
        }

        private char[,,,] InitilaizeSpace4D(char[,] input)
        {
            int size = input.GetLength(0) + 15;
            int baseX, baseY, baseZ, baseW;

            baseX = baseY = baseZ = baseW = (size / 2);
            var cube = new char[size, size,size,size];
            for(int w = 0; w < size; w++)
                for(int z = 0; z < size; z++)
                    for(int y = 0; y < size; y++)
                        for(int x = 0; x < size; x++)
                            cube[w,z,y,x] = '.';
            
            for(int y = -4; y < 4; y++)
                for(int x = -4; x < 4; x++)
                    cube[baseW, baseZ, baseY + y, baseX + x] = input[y + 4, x + 4];
            
            return cube;
        }

        public override string SolveSecondPuzzle()
        {
            var input = Input.AsCharMatrix();
            var cube = InitilaizeSpace4D(input); 
            var neighbourCoords4d = InitilaizeNeighbours4D();

            int size = cube.GetLength(0);

            for(int i = 0; i < 6; i++)
            {
                // Printer.Print(cube);

                var newCube = new char[cube.GetLength(0), cube.GetLength(1), cube.GetLength(2), cube.GetLength(3)];
                Array.Copy(cube, newCube, newCube.Length);

                for(int w = 0; w < size; w++)
                for(int z = 0; z < size; z++)
                {  
                    for(int y = 0; y < size; y++)
                    {
                        for(int x = 0; x < size; x++)
                        {
                            var curr = cube[w,z,y,x];
                            var activeCount = GenerateNeighbours4D((w,z,y,x), size, neighbourCoords4d).Count(x=> cube[x.w, x.z, x.y, x.x] == '#');
                            
                            char newState = '.';

                            if(curr == '#')
                                newState = (activeCount == 2 || activeCount == 3) ? '#' : '.';
                            else
                                newState = activeCount == 3 ? '#' : '.';
                            
                            newCube[w,z,y,x] = newState;
                        }
                    }    
                }

                cube = newCube;
            }

            int count = 0;

            foreach(var (w,z,y,x) in Loop4D(cube))
                if(cube[w,z,y,x] == '#')
                    count++;
            
     
            return count.ToString();
        }
    }
}
