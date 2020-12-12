using System;
using System.Linq;
using aoc_core;

namespace aoc_2020
{
    public class Day12 : AdventPuzzle
    {
        private readonly char[] _sides = new char[] { 'E', 'S', 'W', 'N' };
        public override string SolveFirstPuzzle()
        {
            var directions = Input.AsCustomTypeEnumerable(ParseInput);

            int y = 0;
            int x = 0;

            char dir = 'E';

            foreach (var direction in directions)
            {
                if (_sides.Contains(direction.cmd))
                    MoveInDirection(direction, ref x, ref y);
                else if (direction.cmd == 'R' || direction.cmd == 'L')
                    Rotate(direction, ref dir);
                else
                    MoveInDirection((dir, direction.value), ref x, ref y);

            }

            return (Math.Abs(y) + Math.Abs(x)).ToString();
        }

        public override string SolveSecondPuzzle()
        {
            var directions = Input.AsCustomTypeEnumerable(ParseInput);

            int shipY, shipX, wpY, wpX;
            shipY = shipX = 0;

            wpY = 1;
            wpX = 10;

            foreach (var direction in directions)
            {
                if (_sides.Contains(direction.cmd))
                    MoveInDirection(direction, ref wpX, ref wpY);
                else if (direction.cmd == 'R' || direction.cmd == 'L')
                    RotateWaypoint(direction, ref wpY, ref wpX);
                else
                    MoveForward(direction.value, wpX, wpY, ref shipX, ref shipY);
            }

            return (Math.Abs(shipX) + Math.Abs(shipY)).ToString();
        }

        private void Rotate((char dir, int angle) direction, ref char dir)
        {
            int ticks = direction.angle / 90;
            if (direction.dir == 'R')
            {
                int currentIdx = Array.IndexOf(_sides, dir);
                int newIds = (currentIdx + ticks) % 4;
                dir = _sides[newIds];
            }
            else
            {
                int currentIdx = Array.IndexOf(_sides, dir);
                int newIds = ((currentIdx - ticks) + 4) % 4;
                dir = _sides[newIds];
            }
        }

        private void RotateWaypoint((char dir, int angle) direction, ref int wpy, ref int wpx)
        {
            int ticks = direction.angle / 90;

            for (int i = 0; i < ticks; i++)
                Rotate90(direction.dir, ref wpy, ref wpx);

        }

        private void Rotate90(char dir, ref int wpy, ref int wpx)
        {
            if (dir == 'R')
            {
                var tmp = wpx;
                wpx = wpy;
                wpy = tmp * -1;
            }
            else
            {
                var tmp = wpx;
                wpx = wpy * -1;
                wpy = tmp;
            }
        }

        private void MoveInDirection((char dir, int value) direction, ref int x, ref int y)
        {
            switch (direction.dir)
            {
                case 'N': y += direction.value; break;
                case 'S': y -= direction.value; break;
                case 'E': x += direction.value; break;
                case 'W': x -= direction.value; break;
                default: throw new Exception("Unknown direction");
            }
        }

        private void MoveForward(int factor, int wpx, int wpy, ref int x, ref int y)
        {
            y += factor * wpy;
            x += factor * wpx;
        }

        public (char cmd, int value) ParseInput(string line) => (line.First(), int.Parse(line.Substring(1)));

    }
}
