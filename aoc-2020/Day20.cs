using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day20 : AdventPuzzle
    {
        private List<Tile> _tiles;
        private Dictionary<int, int[]> _matches;
        public override void ParseInput()
        {
            _tiles = ParseTiles(Input.AsStringArray());
        }

        private List<Tile> ParseTiles(string[] input)
        {
            int tmpid = 0;
            var tiles = new List<Tile>();
            List<char[]> tmpTile = new List<char[]>();
            foreach (var line in input)
            {
                if (line.StartsWith("Tile"))
                    tmpid = int.Parse(Regex.Match(line, @"^Tile (\d+):$").Groups[1].Value);
                else if (!string.IsNullOrEmpty(line))
                    tmpTile.Add(line.ToCharArray());
                else
                {
                    tiles.Add(new Tile(tmpid, tmpTile.ToArray()));
                    tmpTile.Clear();
                }

            }
            tiles.Add(new Tile(tmpid, tmpTile.ToArray()));
            return tiles;
        }
        public override string SolveFirstPuzzle()
        {
            var bordersList = new List<HashSet<string>>();

            FindBorders();

            var prod = _matches.Where(x => x.Value.Count() == 2).ToList();
            return ((long)prod[0].Key * prod[1].Key * prod[2].Key * prod[3].Key).ToString();
        }

        public override string SolveSecondPuzzle()
        {
            if (_matches == null)
                FindBorders();

            var puzzle = SolvePuzzle();
            var combinedPuzzle = CombinePieces(puzzle);


            var monsterRegex = new Regex(
                                                                                            "(..................#." +
                 "............................................................................#....##....##....###" +
                 ".............................................................................#..#..#..#..#..#...)");
            // var monsterRegex = new Regex("(....................#.....#....##....##....###.....#..#..#..#..#..#.....)");
            var imageWithMonsters = FindMonsters(monsterRegex, combinedPuzzle);
            var count = CountMonsters(monsterRegex, imageWithMonsters);

            var monsters = count * 15; // 15 #s in each monster
            var total = imageWithMonsters.Count(x => x == '#') - monsters;

            return total.ToString();
        }

        private int CountMonsters(Regex monsterRegex, string imageWithMonsters)
        {
            int count = 0;
            var m = monsterRegex.Match(imageWithMonsters);
            while (m.Success)
            {
                count++;
                m = monsterRegex.Match(imageWithMonsters, m.Index + 1);
            }

            return count;
        }
        private string FindMonsters(Regex monsterRegex, string combinedPuzzle)
        {
            Tile t = ParseTiles((combinedPuzzle).Split(Environment.NewLine))[0];
            string oneliner = t.GetOnelineImage();
            int rotations = 0;

            while (!monsterRegex.IsMatch(oneliner))
            {
                RotateAndFlip(rotations++, ref t);
                oneliner = t.GetOnelineImage();
            }

            return oneliner;
        }

        private void RotateAndFlip(int rotations, ref Tile t)
        {
            if (rotations == 8)
                throw new Exception("No match found!");

            if (rotations == 3)
                t.FlipHorizontal();

            t.Rotate();
        }
        private string CombinePieces(List<(Tile, (int, int))> puzzle)
        {
            StringBuilder solved = new StringBuilder();
            var size = puzzle.Max(x => x.Item2.Item1);
            for (int i = size; i >= 0; i--)
            {
                var pieces = puzzle.Where(x => x.Item2.Item1 == i)
                    .OrderBy(x => x.Item2.Item2)
                    .Select(x => x.Item1.Image)
                    .ToArray();

                for (int k = 1; k < pieces[0][0].Length - 1; k++)
                {
                    foreach (var piece in pieces)
                        solved.Append(string.Join("", piece[k][1..^1]));

                    solved.Append(Environment.NewLine);
                }
            }

            return solved.ToString().TrimEnd();
        }

        private List<(Tile, (int, int))> SolvePuzzle()
        {
            var root = _tiles.Single(x => x.Id == _matches.Where(x => x.Value.Count() == 2).First().Key);
            var q = new Queue<Tile>();
            HashSet<Tile> discovered = new HashSet<Tile>();
            discovered.Add(root);
            var positions = new List<(Tile, (int, int))>();

            var currPos = (0, 0);
            positions.Add((root, currPos));

            q.Enqueue(root);
            while (q.Any())
            {
                var v = q.Dequeue();
                currPos = positions.Single(x => x.Item1.Id == v.Id).Item2;

                foreach (var neighId in _matches[v.Id])
                {
                    if (positions.Any(x => x.Item1.Id == neighId))
                        continue;

                    var neighTile = _tiles.Single(x => x.Id == neighId);
                    bool isMatch = false;
                    int rotations = 0;
                    while (!isMatch)
                    {
                        var matchingBorder = FindMatchingBorder(v, neighTile);
                        isMatch = matchingBorder != 'n';

                        if (isMatch)
                        {
                            (int, int) newPos;
                            switch (matchingBorder)
                            {
                                case 'u':
                                    newPos = (currPos.Item1 + 1, currPos.Item2);
                                    break;
                                case 'd':
                                    newPos = (currPos.Item1 - 1, currPos.Item2);
                                    break;
                                case 'l':
                                    newPos = (currPos.Item1, currPos.Item2 - 1);
                                    break;
                                case 'r':
                                    newPos = (currPos.Item1, currPos.Item2 + 1);
                                    break;
                                default:
                                    throw new Exception("something is wrong");

                            }

                            positions.Add((neighTile, newPos));
                            q.Enqueue(neighTile);
                        }
                        else
                        {
                            RotateAndFlip(rotations++, ref neighTile);
                        }
                    }
                }
            }

            return positions;
        }

        private char FindMatchingBorder(Tile first, Tile second)
        {
            if (first.Borders.u == second.Borders.d)
                return 'u';
            else if (first.Borders.d == second.Borders.u)
                return 'd';
            else if (first.Borders.l == second.Borders.r)
                return 'l';
            else if (first.Borders.r == second.Borders.l)
                return 'r';
            else return 'n';
        }
        private void FindBorders()
        {
            _matches = new Dictionary<int, int[]>();
            for (int i = 0; i < _tiles.Count(); i++)
            {
                var curr = _tiles[i];
                var currBorders = curr.BordersArray.ToList();
                currBorders.AddRange(curr.BordersArray.Select(x => new string(x.Reverse().ToArray())));
                var cb = currBorders.ToHashSet();

                var otherBorders = _tiles.Where(x => x.Id != curr.Id)
                    .Select(x => (x.Id, x.BordersArray))
                    .ToList();

                otherBorders.AddRange(_tiles.Where(x => x.Id != curr.Id)
                    .Select(x => (x.Id, x.BordersArray.Select(y => string.Join("", y.Reverse())).ToArray())));

                var matches = otherBorders.Where(x => x.BordersArray.Any(y => curr.BordersArray.Contains(y)));
                _matches[curr.Id] = matches.Select(x => x.Id).ToArray();
            }
        }
    }

    public class Tile
    {
        public int Id { get; }
        public char[][] Image { get; set; }
        public (string u, string r, string d, string l) Borders
        {
            get { return (borders[0], borders[1], borders[2], borders[3]); }
        }
        public string GetOnelineImage()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < Image.Length; y++)
                sb.Append(string.Join("", Image[y]));

            return sb.ToString();
        }
        public string[] BordersArray { get { return borders; } }
        private string[] borders;
        public Tile(int id, char[][] image)
        {
            Id = id;
            Image = image;

            ReadBorders();
        }
        private void ReadBorders()
        {
            borders = new string[4];
            for (int i = 0; i < Image.Length; i++)
            {
                borders[0] += Image[0][i];
                borders[1] += Image[i][Image.Length - 1];
                borders[2] += Image[Image.Length - 1][i];
                borders[3] += Image[i][0];
            }
        }
        public void Rotate()
        {
            var newImage = new char[Image.Length][];

            for (int i = 0; i < newImage.Length; i++)
            {
                newImage[i] = new char[newImage.Length];
                for (int j = 0; j < newImage.Length; j++)
                    newImage[i][j] = Image[newImage.Length - j - 1][i];
            }

            Image = newImage;
            ReadBorders();
        }
        public void FlipHorizontal()
        {
            var newImage = new char[Image.Length][];

            for (int i = 0; i < newImage.Length; i++)
            {
                newImage[i] = Image[i].Reverse().ToArray();
            }

            Image = newImage;
            ReadBorders();
        }
    }
}
