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

        public override void ParseInput()
        {
            int tmpid = 0;
            _tiles = new List<Tile>();
            List<char[]> tmpTile = new List<char[]>();
            foreach(var line in Input.AsStringArray())
            {    
                if(line.StartsWith("Tile"))
                    tmpid = int.Parse(Regex.Match(line, @"^Tile (\d+):$").Groups[1].Value);
                else if(!string.IsNullOrEmpty(line))
                    tmpTile.Add(line.ToCharArray());
                else
                {
                    _tiles.Add(new Tile(tmpid, tmpTile.ToArray()));
                    tmpTile.Clear();
                }
                
            }
             _tiles.Add(new Tile(tmpid, tmpTile.ToArray()));
        }
        public override string SolveFirstPuzzle()
        {
            var bordersList = new List<HashSet<string>>();

            var counts = new Dictionary<int, int>();
            
            for(int i = 0; i < _tiles.Count(); i++)
            {
                var curr = _tiles[i];
                var currBorders = curr.BordersArray.ToList();
                currBorders.AddRange(curr.BordersArray.Select(x => new string(x.Reverse().ToArray())));
                var cb = currBorders.ToHashSet();

                var otherBorders = _tiles.Where(x=>x.Id != curr.Id).SelectMany(x => x.BordersArray).ToList();
                otherBorders.AddRange(_tiles.Where(x=>x.Id != curr.Id)
                    .SelectMany(x => x.BordersArray)
                    .Select(x => new string(x.Reverse().ToArray())));
                
                var countMatching = curr.BordersArray.Count(x=> otherBorders.Contains(x));

                counts[curr.Id] = countMatching;

            }

            var prod = counts.Where(x => x.Value == 2).ToList();
            return ((long)prod[0].Key * prod[1].Key * prod[2].Key * prod[3].Key).ToString();
        }

        public override string SolveSecondPuzzle()
        {
            throw new NotImplementedException();
        }
    }

    public class Tile{
        public int Id {get; }
        char[][] Image {get;}
        (string u, string r, string d, string l) Borders { get 
            { return (borders[0], borders[1], borders[2], borders[3]);}}
        // public HashSet<string> BordersBag {get; set;}
        public string[] BordersArray {get {return borders;}}
        private string[] borders;
        public Tile(int id, char[][] image)
        {
            Id = id;
            Image = image;

            string u, r, d, l;
            u = r = d = l = "";

            borders = new string[4];
            for(int i = 0; i < image.Length; i++)
            {
                borders[0] += image[0][i];
                borders[1] += image[i][image.Length -1];
                borders[2] += image[image.Length -1][i];
                borders[3] += image[i][0];
            }
        }
        public void Rotate()
        {
            var tmp = borders[3];
            borders[3] = borders[2];
            borders[2] = string.Join("", borders[1].Reverse());
            borders[1] = borders[0];
            borders[0] = string.Join("",tmp.Reverse());
        }

        public void FlipVertical()
        {
            //  (string u, string r, string d, string l)
            //  (borders[0], borders[1], borders[2], borders[3])
            var tmp = borders[0];
            borders[0] = borders[2];
            borders[2] = tmp;

            borders[1] = string.Join("",borders[1].Reverse());
            borders[3] = string.Join("",borders[3].Reverse());
        }
        public void FlipHorizontal()
        {
            //  (string u, string r, string d, string l)
            //  (borders[0], borders[1], borders[2], borders[3])
             var tmp = borders[1];
            borders[1] = borders[3];
            borders[3] = tmp;

            borders[0] = string.Join("",borders[0].Reverse());
            borders[2] = string.Join("",borders[2].Reverse());
        }
        
    }
}
