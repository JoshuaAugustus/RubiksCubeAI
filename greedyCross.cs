using System;

using System.Collections.Generic;

using System.Collections;

using System.Text;

using static System.Text.StringBuilder;

using static System.Random;



public class Rubik
{

    public static void Main(String[] args)
    {

        Cube c = new Cube(15);

        Console.WriteLine(c.ToString());

        foreach (int color in c.getShufflePath())
        {

            Console.Write(color);

            Console.Write(" ");

        }

        Console.WriteLine();

        //Console.WriteLine(DepthFirst.DepthFirstSearch(0, 8, c, "", ""));

        List<Cube> start = new List<Cube>();

        start.Add(c);

        List<Cube> lst = new List<Cube>();

        //Cube c2 = DepthFirst.Greedy(0,1000,start,lst);

        //Console.WriteLine(c2);
        Cube c2 = DepthFirst.greedyCross(start, lst);
        Console.WriteLine(c2);
        foreach (int rotate in c2.getSolvePath())
        {
            Console.Write(rotate + " ");
        }
        c2 = DepthFirst.WhiteCorner(c2);
        Console.WriteLine(c2);
        Console.WriteLine();


    }



    class Cube
    {

        public int depth;

        public Side front, back, left, right, top, bottom, holder;

        private List<int> shufflePath, solvePath;



        //Color Map Reference

        //Black  : Blank  : 0

        //Green  : Front  : 1

        //Blue   : Back   : 2

        //Orange : Left   : 3

        //Red    : Right  : 4

        //White  : Top    : 5

        //Yellow : Bottom : 6



        //Shuffle/Solve Map Reference

        //rotateLeftClockwise 		 : Turn Left/Orange Clockwise 			: 0

        //rotateLeftCounterClockwise 	 : Turn Left/Orange Counter Clockwise 	: 1

        //rotateRightClockwise 		 : Turn Right/Red Clockwise 			: 2

        //rotateRightCounterClockwise  : Turn Right/Red Counter Clockwise 	: 3

        //rotateTopClockwise 			 : Turn Top/White Clockwise 			: 4

        //rotateTopCounterClockwise 	 : Turn Top/White Counter Clockwise 	: 5

        //rotateBottomClockwise 		 : Turn Bottom/Yellow Clockwise 		: 6

        //rotateBottomCounterClockwise : Turn Bottom/Yellow Counter Clockwise : 7

        //rotateFrontClockwise 		 : Turn Front/Green Clockwise 			: 8

        //rotateFrontCounterClockwise  : Turn Front/Green Counter Clockwise 	: 9

        //rotateBackClockwise 		 : Turn Back/Blue Clockwise 			: 10

        //rotateBackCounterClockwise   : Turn Back/Blue Counter Clockwise 	: 11





        /*

        * Creates a new Cube object and shuffles it <shuffleValue> times. 

        * Setting <shuffleValue> to 0 will create a solved Cube.

        * 

        * @param shuffleValue: Number of moves shuffle() will make

        */

        public Cube(int shuffleValue)
        {

            this.top = new Side(5);

            this.bottom = new Side(6);

            this.front = new Side(1);

            this.back = new Side(2);

            this.left = new Side(3);

            this.right = new Side(4);

            this.holder = new Side(0);

            this.shufflePath = new List<int>();

            this.solvePath = new List<int>();



            this.shuffle(shuffleValue);

            this.solvePath.Clear();

        }



        public Cube(Cube clone)
        {

            this.top = new Side(5);

            this.bottom = new Side(6);

            this.front = new Side(1);

            this.back = new Side(2);

            this.left = new Side(3);

            this.right = new Side(4);

            this.holder = new Side(0);

            this.shufflePath = new List<int>();

            this.solvePath = new List<int>();



            for (int i = 0; i < 9; i++)
            {

                this.top.getSquare(i).setColor(clone.top.getSquare(i).getColor());

                this.bottom.getSquare(i).setColor(clone.bottom.getSquare(i).getColor());

                this.front.getSquare(i).setColor(clone.front.getSquare(i).getColor());

                this.back.getSquare(i).setColor(clone.back.getSquare(i).getColor());

                this.left.getSquare(i).setColor(clone.left.getSquare(i).getColor());

                this.right.getSquare(i).setColor(clone.right.getSquare(i).getColor());

            }



            for (int j = 0; j < clone.getSolvePath().Count; j++)
            {

                this.solvePath.Add(clone.getSolvePath()[j]);

            }



            for (int k = 0; k < clone.getShufflePath().Count; k++)
            {

                this.shufflePath.Add(clone.getShufflePath()[k]);

            }

        }

        /*

        * Randomly shuffles the Cube object <sAmount> times.

        * Will only shuffle clockwise to avoid redundancy.

        * Adds the corresponding path number to the Cube's shufflePath list after each move.

        * Writes <sAmount> to console when finished.

        *

        * @ param sAmount: Number of random moves shuffle will make

        */

        public void shuffle(int sAmount)
        {

            Random randomNumber = new Random();

            int shuffleAmount = sAmount;

            int shuffleValue;

            for (int i = 0; i < shuffleAmount; i++)
            {

                shuffleValue = randomNumber.Next(12) % 6;

                Console.Write(shuffleValue + " ");

                switch (shuffleValue)
                {

                    case 0:

                        this.rotateLeftClockwise();

                        this.shufflePath.Add(0);

                        break;

                    case 1:

                        this.rotateRightClockwise();

                        this.shufflePath.Add(2);

                        break;

                    case 2:

                        this.rotateTopClockwise();

                        this.shufflePath.Add(4);

                        break;

                    case 3:

                        this.rotateBottomClockwise();

                        this.shufflePath.Add(6);

                        break;

                    case 4:

                        this.rotateFrontClockwise();

                        this.shufflePath.Add(8);

                        break;

                    case 5:

                        this.rotateBackClockwise();

                        this.shufflePath.Add(10);

                        break;

                }

            }

            Console.WriteLine();

            Console.WriteLine("Cube shuffled " + sAmount + " times.");

        }



        /* 

        * Shuffles the left/orange Side of the Cube object in a clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public void rotateLeftClockwise()
        {

            holder.getSquare(0).setColor(front.getSquare(0).getColor());

            holder.getSquare(3).setColor(front.getSquare(3).getColor());

            holder.getSquare(6).setColor(front.getSquare(6).getColor());



            front.getSquare(0).setColor(top.getSquare(0).getColor());

            front.getSquare(3).setColor(top.getSquare(3).getColor());

            front.getSquare(6).setColor(top.getSquare(6).getColor());



            top.getSquare(0).setColor(back.getSquare(8).getColor());

            top.getSquare(3).setColor(back.getSquare(5).getColor());

            top.getSquare(6).setColor(back.getSquare(2).getColor());



            back.getSquare(2).setColor(bottom.getSquare(6).getColor());

            back.getSquare(5).setColor(bottom.getSquare(3).getColor());

            back.getSquare(8).setColor(bottom.getSquare(0).getColor());



            bottom.getSquare(0).setColor(holder.getSquare(0).getColor());

            bottom.getSquare(3).setColor(holder.getSquare(3).getColor());

            bottom.getSquare(6).setColor(holder.getSquare(6).getColor());

            holder.getSquare(0).setColor(left.getSquare(0).getColor());
            holder.getSquare(1).setColor(left.getSquare(1).getColor());
            holder.getSquare(2).setColor(left.getSquare(2).getColor());

            left.getSquare(0).setColor(left.getSquare(6).getColor());
            left.getSquare(1).setColor(left.getSquare(3).getColor());
            left.getSquare(2).setColor(holder.getSquare(0).getColor());

            left.getSquare(3).setColor(left.getSquare(7).getColor());
            left.getSquare(6).setColor(left.getSquare(8).getColor());

            left.getSquare(7).setColor(left.getSquare(5).getColor());
            left.getSquare(8).setColor(holder.getSquare(2).getColor());

            left.getSquare(5).setColor(holder.getSquare(1).getColor());
            left.getSquare(2).setColor(holder.getSquare(0).getColor());


            this.solvePath.Add(0);

        }



        /* 

        * Shuffles the left/orange Side of the Cube object in a counter clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public void rotateLeftCounterClockwise()
        {

            holder.getSquare(0).setColor(front.getSquare(0).getColor());

            holder.getSquare(3).setColor(front.getSquare(3).getColor());

            holder.getSquare(6).setColor(front.getSquare(6).getColor());



            front.getSquare(0).setColor(bottom.getSquare(0).getColor());

            front.getSquare(3).setColor(bottom.getSquare(3).getColor());

            front.getSquare(6).setColor(bottom.getSquare(6).getColor());



            bottom.getSquare(0).setColor(back.getSquare(8).getColor());

            bottom.getSquare(3).setColor(back.getSquare(5).getColor());

            bottom.getSquare(6).setColor(back.getSquare(2).getColor());



            back.getSquare(2).setColor(top.getSquare(6).getColor());

            back.getSquare(5).setColor(top.getSquare(3).getColor());

            back.getSquare(8).setColor(top.getSquare(0).getColor());



            top.getSquare(0).setColor(holder.getSquare(0).getColor());

            top.getSquare(3).setColor(holder.getSquare(3).getColor());

            top.getSquare(6).setColor(holder.getSquare(6).getColor());

            holder.getSquare(0).setColor(left.getSquare(0).getColor());
            holder.getSquare(1).setColor(left.getSquare(1).getColor());
            holder.getSquare(2).setColor(left.getSquare(2).getColor());

            left.getSquare(0).setColor(holder.getSquare(2).getColor());
            left.getSquare(1).setColor(left.getSquare(5).getColor());
            left.getSquare(2).setColor(left.getSquare(8).getColor());

            left.getSquare(5).setColor(left.getSquare(7).getColor());
            left.getSquare(8).setColor(left.getSquare(6).getColor());

            left.getSquare(7).setColor(left.getSquare(3).getColor());
            left.getSquare(6).setColor(holder.getSquare(0).getColor());

            left.getSquare(3).setColor(holder.getSquare(1).getColor());
            left.getSquare(0).setColor(holder.getSquare(2).getColor());

            this.solvePath.Add(1);

        }



        /* 

        * Shuffles the right/red Side of the Cube object in a clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public void rotateRightClockwise()
        {

            holder.getSquare(2).setColor(front.getSquare(2).getColor());

            holder.getSquare(5).setColor(front.getSquare(5).getColor());

            holder.getSquare(8).setColor(front.getSquare(8).getColor());



            front.getSquare(2).setColor(bottom.getSquare(2).getColor());

            front.getSquare(5).setColor(bottom.getSquare(5).getColor());

            front.getSquare(8).setColor(bottom.getSquare(8).getColor());



            bottom.getSquare(2).setColor(back.getSquare(6).getColor());

            bottom.getSquare(5).setColor(back.getSquare(3).getColor());

            bottom.getSquare(8).setColor(back.getSquare(0).getColor());



            back.getSquare(0).setColor(top.getSquare(8).getColor());

            back.getSquare(3).setColor(top.getSquare(5).getColor());

            back.getSquare(6).setColor(top.getSquare(2).getColor());



            top.getSquare(2).setColor(holder.getSquare(2).getColor());

            top.getSquare(5).setColor(holder.getSquare(5).getColor());

            top.getSquare(8).setColor(holder.getSquare(8).getColor());

            holder.getSquare(0).setColor(right.getSquare(0).getColor());
            holder.getSquare(1).setColor(right.getSquare(1).getColor());
            holder.getSquare(2).setColor(right.getSquare(2).getColor());

            right.getSquare(0).setColor(right.getSquare(6).getColor());
            right.getSquare(1).setColor(right.getSquare(3).getColor());
            right.getSquare(2).setColor(holder.getSquare(0).getColor());

            right.getSquare(3).setColor(right.getSquare(7).getColor());
            right.getSquare(6).setColor(right.getSquare(8).getColor());

            right.getSquare(7).setColor(right.getSquare(5).getColor());
            right.getSquare(8).setColor(holder.getSquare(2).getColor());

            right.getSquare(5).setColor(holder.getSquare(1).getColor());
            right.getSquare(2).setColor(holder.getSquare(0).getColor());

            this.solvePath.Add(2);

        }



        /* 

        * Shuffles the right/red Side of the Cube object in a counter clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public void rotateRightCounterClockwise()
        {

            holder.getSquare(2).setColor(front.getSquare(2).getColor());

            holder.getSquare(5).setColor(front.getSquare(5).getColor());

            holder.getSquare(8).setColor(front.getSquare(8).getColor());



            front.getSquare(2).setColor(top.getSquare(2).getColor());

            front.getSquare(5).setColor(top.getSquare(5).getColor());

            front.getSquare(8).setColor(top.getSquare(8).getColor());



            top.getSquare(2).setColor(back.getSquare(6).getColor());

            top.getSquare(5).setColor(back.getSquare(3).getColor());

            top.getSquare(8).setColor(back.getSquare(0).getColor());



            back.getSquare(0).setColor(bottom.getSquare(8).getColor());

            back.getSquare(3).setColor(bottom.getSquare(5).getColor());

            back.getSquare(6).setColor(bottom.getSquare(2).getColor());



            bottom.getSquare(2).setColor(holder.getSquare(2).getColor());

            bottom.getSquare(5).setColor(holder.getSquare(5).getColor());

            bottom.getSquare(8).setColor(holder.getSquare(8).getColor());

            holder.getSquare(0).setColor(right.getSquare(0).getColor());
            holder.getSquare(1).setColor(right.getSquare(1).getColor());
            holder.getSquare(2).setColor(right.getSquare(2).getColor());

            right.getSquare(0).setColor(holder.getSquare(2).getColor());
            right.getSquare(1).setColor(right.getSquare(5).getColor());
            right.getSquare(2).setColor(right.getSquare(8).getColor());

            right.getSquare(5).setColor(right.getSquare(7).getColor());
            right.getSquare(8).setColor(right.getSquare(6).getColor());

            right.getSquare(7).setColor(right.getSquare(3).getColor());
            right.getSquare(6).setColor(holder.getSquare(0).getColor());

            right.getSquare(3).setColor(holder.getSquare(1).getColor());
            right.getSquare(0).setColor(holder.getSquare(2).getColor());

            this.solvePath.Add(3);

        }



        /* 

        * Shuffles the top/white Side of the Cube object in a clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public void rotateTopClockwise()
        {

            for (int i = 0; i < 3; i++)
            {

                holder.getSquare(i).setColor(front.getSquare(i).getColor());

                front.getSquare(i).setColor(right.getSquare(i).getColor());

                right.getSquare(i).setColor(back.getSquare(i).getColor());

                back.getSquare(i).setColor(left.getSquare(i).getColor());

                left.getSquare(i).setColor(holder.getSquare(i).getColor());

            }

            holder.getSquare(0).setColor(top.getSquare(0).getColor());
            holder.getSquare(1).setColor(top.getSquare(1).getColor());
            holder.getSquare(2).setColor(top.getSquare(2).getColor());

            top.getSquare(0).setColor(top.getSquare(6).getColor());
            top.getSquare(1).setColor(top.getSquare(3).getColor());
            top.getSquare(2).setColor(holder.getSquare(0).getColor());

            top.getSquare(3).setColor(top.getSquare(7).getColor());
            top.getSquare(6).setColor(top.getSquare(8).getColor());

            top.getSquare(7).setColor(top.getSquare(5).getColor());
            top.getSquare(8).setColor(holder.getSquare(2).getColor());

            top.getSquare(5).setColor(holder.getSquare(1).getColor());
            top.getSquare(2).setColor(holder.getSquare(0).getColor());

            this.solvePath.Add(4);

        }



        /* 

        * Shuffles the top/white Side of the Cube object in a counter clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public void rotateTopCounterClockwise()
        {

            for (int i = 0; i < 3; i++)
            {

                holder.getSquare(i).setColor(front.getSquare(i).getColor());

                front.getSquare(i).setColor(left.getSquare(i).getColor());

                left.getSquare(i).setColor(back.getSquare(i).getColor());

                back.getSquare(i).setColor(right.getSquare(i).getColor());

                right.getSquare(i).setColor(holder.getSquare(i).getColor());

            }

            holder.getSquare(0).setColor(top.getSquare(0).getColor());
            holder.getSquare(1).setColor(top.getSquare(1).getColor());
            holder.getSquare(2).setColor(top.getSquare(2).getColor());

            top.getSquare(0).setColor(holder.getSquare(2).getColor());
            top.getSquare(1).setColor(top.getSquare(5).getColor());
            top.getSquare(2).setColor(top.getSquare(8).getColor());

            top.getSquare(5).setColor(top.getSquare(7).getColor());
            top.getSquare(8).setColor(top.getSquare(6).getColor());

            top.getSquare(7).setColor(top.getSquare(3).getColor());
            top.getSquare(6).setColor(holder.getSquare(0).getColor());

            top.getSquare(3).setColor(holder.getSquare(1).getColor());
            top.getSquare(0).setColor(holder.getSquare(2).getColor());

            this.solvePath.Add(5);

        }



        /* 

        * Shuffles the bottom/yellow Side of the Cube object in a clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public void rotateBottomClockwise()
        {

            for (int i = 6; i < 9; i++)
            {

                holder.getSquare(i).setColor(front.getSquare(i).getColor());

                front.getSquare(i).setColor(left.getSquare(i).getColor());

                left.getSquare(i).setColor(back.getSquare(i).getColor());

                back.getSquare(i).setColor(right.getSquare(i).getColor());

                right.getSquare(i).setColor(holder.getSquare(i).getColor());

            }

            holder.getSquare(0).setColor(bottom.getSquare(0).getColor());
            holder.getSquare(1).setColor(bottom.getSquare(1).getColor());
            holder.getSquare(2).setColor(bottom.getSquare(2).getColor());

            bottom.getSquare(0).setColor(bottom.getSquare(6).getColor());
            bottom.getSquare(1).setColor(bottom.getSquare(3).getColor());
            bottom.getSquare(2).setColor(holder.getSquare(0).getColor());

            bottom.getSquare(3).setColor(bottom.getSquare(7).getColor());
            bottom.getSquare(6).setColor(bottom.getSquare(8).getColor());

            bottom.getSquare(7).setColor(bottom.getSquare(5).getColor());
            bottom.getSquare(8).setColor(holder.getSquare(2).getColor());

            bottom.getSquare(5).setColor(holder.getSquare(1).getColor());
            bottom.getSquare(2).setColor(holder.getSquare(0).getColor());

            this.solvePath.Add(6);

        }



        /* 

        * Shuffles the bottom/yellow Side of the Cube object in a count clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public void rotateBottomCounterClockwise()
        {

            for (int i = 6; i < 9; i++)
            {

                holder.getSquare(i).setColor(front.getSquare(i).getColor());

                front.getSquare(i).setColor(right.getSquare(i).getColor());

                right.getSquare(i).setColor(back.getSquare(i).getColor());

                back.getSquare(i).setColor(left.getSquare(i).getColor());

                left.getSquare(i).setColor(holder.getSquare(i).getColor());

            }

            holder.getSquare(0).setColor(bottom.getSquare(0).getColor());
            holder.getSquare(1).setColor(bottom.getSquare(1).getColor());
            holder.getSquare(2).setColor(bottom.getSquare(2).getColor());

            bottom.getSquare(0).setColor(holder.getSquare(2).getColor());
            bottom.getSquare(1).setColor(bottom.getSquare(5).getColor());
            bottom.getSquare(2).setColor(bottom.getSquare(8).getColor());

            bottom.getSquare(5).setColor(bottom.getSquare(7).getColor());
            bottom.getSquare(8).setColor(bottom.getSquare(6).getColor());

            bottom.getSquare(7).setColor(bottom.getSquare(3).getColor());
            bottom.getSquare(6).setColor(holder.getSquare(0).getColor());

            bottom.getSquare(3).setColor(holder.getSquare(1).getColor());
            bottom.getSquare(0).setColor(holder.getSquare(2).getColor());

            this.solvePath.Add(7);

        }



        /* 

        * Shuffles the front/green Side of the Cube object in a clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public void rotateFrontClockwise()
        {

            for (int i = 6; i < 9; i++)
            {

                holder.getSquare(i).setColor(top.getSquare(i).getColor());

            }



            top.getSquare(6).setColor(left.getSquare(8).getColor());

            top.getSquare(7).setColor(left.getSquare(5).getColor());

            top.getSquare(8).setColor(left.getSquare(2).getColor());



            left.getSquare(2).setColor(bottom.getSquare(0).getColor());

            left.getSquare(5).setColor(bottom.getSquare(1).getColor());

            left.getSquare(8).setColor(bottom.getSquare(2).getColor());



            bottom.getSquare(0).setColor(right.getSquare(6).getColor());

            bottom.getSquare(1).setColor(right.getSquare(3).getColor());

            bottom.getSquare(2).setColor(right.getSquare(0).getColor());



            right.getSquare(0).setColor(holder.getSquare(6).getColor());

            right.getSquare(3).setColor(holder.getSquare(7).getColor());

            right.getSquare(6).setColor(holder.getSquare(8).getColor());

            holder.getSquare(0).setColor(front.getSquare(0).getColor());
            holder.getSquare(1).setColor(front.getSquare(1).getColor());
            holder.getSquare(2).setColor(front.getSquare(2).getColor());

            front.getSquare(0).setColor(front.getSquare(6).getColor());
            front.getSquare(1).setColor(front.getSquare(3).getColor());
            front.getSquare(2).setColor(holder.getSquare(0).getColor());

            front.getSquare(3).setColor(front.getSquare(7).getColor());
            front.getSquare(6).setColor(front.getSquare(8).getColor());

            front.getSquare(7).setColor(front.getSquare(5).getColor());
            front.getSquare(8).setColor(holder.getSquare(2).getColor());

            front.getSquare(5).setColor(holder.getSquare(1).getColor());
            front.getSquare(2).setColor(holder.getSquare(0).getColor());

            this.solvePath.Add(8);

        }



        /* 

        * Shuffles the front/green Side of the Cube object in a counter clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public void rotateFrontCounterClockwise()
        {

            for (int i = 6; i < 9; i++)
            {

                holder.getSquare(i).setColor(top.getSquare(i).getColor());

            }



            top.getSquare(6).setColor(right.getSquare(0).getColor());

            top.getSquare(7).setColor(right.getSquare(3).getColor());

            top.getSquare(8).setColor(right.getSquare(6).getColor());



            right.getSquare(0).setColor(bottom.getSquare(2).getColor());

            right.getSquare(3).setColor(bottom.getSquare(1).getColor());

            right.getSquare(6).setColor(bottom.getSquare(0).getColor());



            bottom.getSquare(0).setColor(left.getSquare(2).getColor());

            bottom.getSquare(1).setColor(left.getSquare(5).getColor());

            bottom.getSquare(2).setColor(left.getSquare(8).getColor());



            left.getSquare(2).setColor(holder.getSquare(8).getColor());

            left.getSquare(5).setColor(holder.getSquare(7).getColor());

            left.getSquare(8).setColor(holder.getSquare(6).getColor());

            holder.getSquare(0).setColor(front.getSquare(0).getColor());
            holder.getSquare(1).setColor(front.getSquare(1).getColor());
            holder.getSquare(2).setColor(front.getSquare(2).getColor());

            front.getSquare(0).setColor(holder.getSquare(2).getColor());
            front.getSquare(1).setColor(front.getSquare(5).getColor());
            front.getSquare(2).setColor(front.getSquare(8).getColor());

            front.getSquare(5).setColor(front.getSquare(7).getColor());
            front.getSquare(8).setColor(front.getSquare(6).getColor());

            front.getSquare(7).setColor(front.getSquare(3).getColor());
            front.getSquare(6).setColor(holder.getSquare(0).getColor());

            front.getSquare(3).setColor(holder.getSquare(1).getColor());
            front.getSquare(0).setColor(holder.getSquare(2).getColor());

            this.solvePath.Add(9);

        }



        /* 

        * Shuffles the back/blue Side of the Cube object in a clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public void rotateBackClockwise()
        {

            for (int i = 0; i < 3; i++)
            {

                holder.getSquare(i).setColor(top.getSquare(i).getColor());

            }



            top.getSquare(0).setColor(right.getSquare(2).getColor());

            top.getSquare(1).setColor(right.getSquare(5).getColor());

            top.getSquare(2).setColor(right.getSquare(8).getColor());



            right.getSquare(2).setColor(bottom.getSquare(8).getColor());

            right.getSquare(5).setColor(bottom.getSquare(7).getColor());

            right.getSquare(8).setColor(bottom.getSquare(6).getColor());



            bottom.getSquare(6).setColor(left.getSquare(0).getColor());

            bottom.getSquare(7).setColor(left.getSquare(3).getColor());

            bottom.getSquare(8).setColor(left.getSquare(6).getColor());



            left.getSquare(0).setColor(holder.getSquare(2).getColor());

            left.getSquare(3).setColor(holder.getSquare(1).getColor());

            left.getSquare(6).setColor(holder.getSquare(0).getColor());

            holder.getSquare(0).setColor(back.getSquare(0).getColor());
            holder.getSquare(1).setColor(back.getSquare(1).getColor());
            holder.getSquare(2).setColor(back.getSquare(2).getColor());

            back.getSquare(0).setColor(back.getSquare(6).getColor());
            back.getSquare(1).setColor(back.getSquare(3).getColor());
            back.getSquare(2).setColor(holder.getSquare(0).getColor());

            back.getSquare(3).setColor(back.getSquare(7).getColor());
            back.getSquare(6).setColor(back.getSquare(8).getColor());

            back.getSquare(7).setColor(back.getSquare(5).getColor());
            back.getSquare(8).setColor(holder.getSquare(2).getColor());

            back.getSquare(5).setColor(holder.getSquare(1).getColor());
            back.getSquare(2).setColor(holder.getSquare(0).getColor());

            this.solvePath.Add(10);

        }



        /* 

        * Shuffles the back/blue Side of the Cube object in a counter clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public void rotateBackCounterClockwise()
        {

            for (int i = 0; i < 3; i++)
            {

                holder.getSquare(i).setColor(top.getSquare(i).getColor());

            }



            top.getSquare(0).setColor(left.getSquare(6).getColor());

            top.getSquare(1).setColor(left.getSquare(3).getColor());

            top.getSquare(2).setColor(left.getSquare(0).getColor());



            left.getSquare(0).setColor(bottom.getSquare(6).getColor());

            left.getSquare(3).setColor(bottom.getSquare(7).getColor());

            left.getSquare(6).setColor(bottom.getSquare(8).getColor());



            bottom.getSquare(6).setColor(right.getSquare(8).getColor());

            bottom.getSquare(7).setColor(right.getSquare(5).getColor());

            bottom.getSquare(8).setColor(right.getSquare(2).getColor());



            right.getSquare(2).setColor(holder.getSquare(0).getColor());

            right.getSquare(5).setColor(holder.getSquare(1).getColor());

            right.getSquare(8).setColor(holder.getSquare(2).getColor());

            holder.getSquare(0).setColor(back.getSquare(0).getColor());
            holder.getSquare(1).setColor(back.getSquare(1).getColor());
            holder.getSquare(2).setColor(back.getSquare(2).getColor());

            back.getSquare(0).setColor(holder.getSquare(2).getColor());
            back.getSquare(1).setColor(back.getSquare(5).getColor());
            back.getSquare(2).setColor(back.getSquare(8).getColor());

            back.getSquare(5).setColor(back.getSquare(7).getColor());
            back.getSquare(8).setColor(back.getSquare(6).getColor());

            back.getSquare(7).setColor(back.getSquare(3).getColor());
            back.getSquare(6).setColor(holder.getSquare(0).getColor());

            back.getSquare(3).setColor(holder.getSquare(1).getColor());
            back.getSquare(0).setColor(holder.getSquare(2).getColor());


            this.solvePath.Add(11);

        }



        public Cube createLeftClockwise()
        {

            Cube clone = new Cube(this);


            /*
            clone.holder.getSquare(0).setColor(clone.front.getSquare(0).getColor());

            clone.holder.getSquare(3).setColor(clone.front.getSquare(3).getColor());

            clone.holder.getSquare(6).setColor(clone.front.getSquare(6).getColor());



            clone.front.getSquare(0).setColor(clone.top.getSquare(0).getColor());

            clone.front.getSquare(3).setColor(clone.top.getSquare(3).getColor());

            clone.front.getSquare(6).setColor(clone.top.getSquare(6).getColor());



            clone.top.getSquare(0).setColor(clone.back.getSquare(8).getColor());

            clone.top.getSquare(3).setColor(clone.back.getSquare(5).getColor());

            clone.top.getSquare(6).setColor(clone.back.getSquare(2).getColor());



            clone.back.getSquare(2).setColor(clone.bottom.getSquare(6).getColor());

            clone.back.getSquare(5).setColor(clone.bottom.getSquare(3).getColor());

            clone.back.getSquare(8).setColor(clone.bottom.getSquare(0).getColor());



            clone.bottom.getSquare(0).setColor(clone.holder.getSquare(0).getColor());

            clone.bottom.getSquare(3).setColor(clone.holder.getSquare(3).getColor());

            clone.bottom.getSquare(6).setColor(clone.holder.getSquare(6).getColor());



            clone.solvePath.Add(0);
            */

            clone.rotateLeftClockwise();

            return clone;

        }



        /* 

        * Shuffles the left/orange Side of the Cube object in a counter clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public Cube createLeftCounterClockwise()
        {

            Cube clone = new Cube(this);


            /*
            clone.holder.getSquare(0).setColor(clone.front.getSquare(0).getColor());

            clone.holder.getSquare(3).setColor(clone.front.getSquare(3).getColor());

            clone.holder.getSquare(6).setColor(clone.front.getSquare(6).getColor());



            clone.front.getSquare(0).setColor(clone.bottom.getSquare(0).getColor());

            clone.front.getSquare(3).setColor(clone.bottom.getSquare(3).getColor());

            clone.front.getSquare(6).setColor(clone.bottom.getSquare(6).getColor());



            clone.bottom.getSquare(0).setColor(clone.back.getSquare(8).getColor());

            clone.bottom.getSquare(3).setColor(clone.back.getSquare(5).getColor());

            clone.bottom.getSquare(6).setColor(clone.back.getSquare(2).getColor());



            clone.back.getSquare(2).setColor(clone.top.getSquare(6).getColor());

            clone.back.getSquare(5).setColor(clone.top.getSquare(3).getColor());

            clone.back.getSquare(8).setColor(clone.top.getSquare(0).getColor());



            clone.top.getSquare(0).setColor(clone.holder.getSquare(0).getColor());

            clone.top.getSquare(3).setColor(clone.holder.getSquare(3).getColor());

            clone.top.getSquare(6).setColor(clone.holder.getSquare(6).getColor());



            clone.solvePath.Add(1);
            */

            clone.rotateLeftCounterClockwise();


            return clone;

        }



        /* 

        * Shuffles the right/red Side of the Cube object in a clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public Cube createRightClockwise()
        {

            Cube clone = new Cube(this);


            /*
            clone.holder.getSquare(2).setColor(clone.front.getSquare(2).getColor());

            clone.holder.getSquare(5).setColor(clone.front.getSquare(5).getColor());

            clone.holder.getSquare(8).setColor(clone.front.getSquare(8).getColor());



            clone.front.getSquare(2).setColor(clone.bottom.getSquare(2).getColor());

            clone.front.getSquare(5).setColor(clone.bottom.getSquare(5).getColor());

            clone.front.getSquare(8).setColor(clone.bottom.getSquare(8).getColor());



            clone.bottom.getSquare(2).setColor(clone.back.getSquare(6).getColor());

            clone.bottom.getSquare(5).setColor(clone.back.getSquare(3).getColor());

            clone.bottom.getSquare(8).setColor(clone.back.getSquare(0).getColor());



            clone.back.getSquare(0).setColor(clone.top.getSquare(8).getColor());

            clone.back.getSquare(3).setColor(clone.top.getSquare(5).getColor());

            clone.back.getSquare(6).setColor(clone.top.getSquare(2).getColor());



            clone.top.getSquare(2).setColor(clone.holder.getSquare(2).getColor());

            clone.top.getSquare(5).setColor(clone.holder.getSquare(5).getColor());

            clone.top.getSquare(8).setColor(clone.holder.getSquare(8).getColor());



            clone.solvePath.Add(2);
            */

            clone.rotateRightClockwise();


            return clone;

        }



        /* 

        * Shuffles the right/red Side of the Cube object in a counter clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public Cube createRightCounterClockwise()
        {

            Cube clone = new Cube(this);


            /*
            clone.holder.getSquare(2).setColor(clone.front.getSquare(2).getColor());

            clone.holder.getSquare(5).setColor(clone.front.getSquare(5).getColor());

            clone.holder.getSquare(8).setColor(clone.front.getSquare(8).getColor());



            clone.front.getSquare(2).setColor(clone.top.getSquare(2).getColor());

            clone.front.getSquare(5).setColor(clone.top.getSquare(5).getColor());

            clone.front.getSquare(8).setColor(clone.top.getSquare(8).getColor());



            clone.top.getSquare(2).setColor(clone.back.getSquare(6).getColor());

            clone.top.getSquare(5).setColor(clone.back.getSquare(3).getColor());

            clone.top.getSquare(8).setColor(clone.back.getSquare(0).getColor());



            clone.back.getSquare(0).setColor(clone.bottom.getSquare(8).getColor());

            clone.back.getSquare(3).setColor(clone.bottom.getSquare(5).getColor());

            clone.back.getSquare(6).setColor(clone.bottom.getSquare(2).getColor());



            clone.bottom.getSquare(2).setColor(clone.holder.getSquare(2).getColor());

            clone.bottom.getSquare(5).setColor(clone.holder.getSquare(5).getColor());

            clone.bottom.getSquare(8).setColor(clone.holder.getSquare(8).getColor());



            clone.solvePath.Add(3);
            */

            clone.rotateRightCounterClockwise();


            return clone;

        }



        /* 

        * Shuffles the top/white Side of the Cube object in a clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public Cube createTopClockwise()
        {

            Cube clone = new Cube(this);


            /*
            for (int i = 0; i < 3; i++)
            {

                clone.holder.getSquare(i).setColor(clone.front.getSquare(i).getColor());

                clone.front.getSquare(i).setColor(clone.right.getSquare(i).getColor());

                clone.right.getSquare(i).setColor(clone.back.getSquare(i).getColor());

                clone.back.getSquare(i).setColor(clone.left.getSquare(i).getColor());

                clone.left.getSquare(i).setColor(clone.holder.getSquare(i).getColor());

            }

            clone.solvePath.Add(4);
            */

            clone.rotateTopClockwise();


            return clone;

        }



        /* 

        * Shuffles the top/white Side of the Cube object in a counter clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public Cube createTopCounterClockwise()
        {

            Cube clone = new Cube(this);


            /*
            for (int i = 0; i < 3; i++)
            {

                clone.holder.getSquare(i).setColor(clone.front.getSquare(i).getColor());

                clone.front.getSquare(i).setColor(clone.left.getSquare(i).getColor());

                clone.left.getSquare(i).setColor(clone.back.getSquare(i).getColor());

                clone.back.getSquare(i).setColor(clone.right.getSquare(i).getColor());

                clone.right.getSquare(i).setColor(clone.holder.getSquare(i).getColor());

            }

            clone.solvePath.Add(5);
            */

            clone.rotateTopCounterClockwise();


            return clone;

        }



        /* 

        * Shuffles the bottom/yellow Side of the Cube object in a clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public Cube createBottomClockwise()
        {

            Cube clone = new Cube(this);


            /*
            for (int i = 6; i < 9; i++)
            {

                clone.holder.getSquare(i).setColor(clone.front.getSquare(i).getColor());

                clone.front.getSquare(i).setColor(clone.left.getSquare(i).getColor());

                clone.left.getSquare(i).setColor(clone.back.getSquare(i).getColor());

                clone.back.getSquare(i).setColor(clone.right.getSquare(i).getColor());

                clone.right.getSquare(i).setColor(clone.holder.getSquare(i).getColor());

            }

            clone.solvePath.Add(6);
            */

            clone.rotateBottomClockwise();


            return clone;

        }



        /* 

        * Shuffles the bottom/yellow Side of the Cube object in a count clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public Cube createBottomCounterClockwise()
        {

            Cube clone = new Cube(this);


            /*
            for (int i = 6; i < 9; i++)
            {

                clone.holder.getSquare(i).setColor(clone.front.getSquare(i).getColor());

                clone.front.getSquare(i).setColor(clone.right.getSquare(i).getColor());

                clone.right.getSquare(i).setColor(clone.back.getSquare(i).getColor());

                clone.back.getSquare(i).setColor(clone.left.getSquare(i).getColor());

                clone.left.getSquare(i).setColor(clone.holder.getSquare(i).getColor());

            }

            clone.solvePath.Add(7);
            */

            clone.rotateBottomCounterClockwise();


            return clone;

        }



        /* 

        * Shuffles the front/green Side of the Cube object in a clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public Cube createFrontClockwise()
        {

            Cube clone = new Cube(this);


            /*
            for (int i = 6; i < 9; i++)
            {

                clone.holder.getSquare(i).setColor(clone.top.getSquare(i).getColor());

            }



            clone.top.getSquare(6).setColor(clone.left.getSquare(8).getColor());

            clone.top.getSquare(7).setColor(clone.left.getSquare(5).getColor());

            clone.top.getSquare(8).setColor(clone.left.getSquare(2).getColor());



            clone.left.getSquare(2).setColor(clone.bottom.getSquare(0).getColor());

            clone.left.getSquare(5).setColor(clone.bottom.getSquare(1).getColor());

            clone.left.getSquare(8).setColor(clone.bottom.getSquare(2).getColor());



            clone.bottom.getSquare(0).setColor(clone.right.getSquare(6).getColor());

            clone.bottom.getSquare(1).setColor(clone.right.getSquare(3).getColor());

            clone.bottom.getSquare(2).setColor(clone.right.getSquare(0).getColor());



            clone.right.getSquare(0).setColor(clone.holder.getSquare(6).getColor());

            clone.right.getSquare(3).setColor(clone.holder.getSquare(7).getColor());

            clone.right.getSquare(6).setColor(clone.holder.getSquare(8).getColor());



            clone.solvePath.Add(8);
            */

            clone.rotateFrontClockwise();


            return clone;

        }



        /* 

        * Shuffles the front/green Side of the Cube object in a counter clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public Cube createFrontCounterClockwise()
        {

            Cube clone = new Cube(this);


            /*
            for (int i = 6; i < 9; i++)
            {

                clone.holder.getSquare(i).setColor(clone.top.getSquare(i).getColor());

            }



            clone.top.getSquare(6).setColor(clone.right.getSquare(0).getColor());

            clone.top.getSquare(7).setColor(clone.right.getSquare(3).getColor());

            clone.top.getSquare(8).setColor(clone.right.getSquare(6).getColor());



            clone.right.getSquare(0).setColor(clone.bottom.getSquare(2).getColor());

            clone.right.getSquare(3).setColor(clone.bottom.getSquare(1).getColor());

            clone.right.getSquare(6).setColor(clone.bottom.getSquare(0).getColor());



            clone.bottom.getSquare(0).setColor(clone.left.getSquare(2).getColor());

            clone.bottom.getSquare(1).setColor(clone.left.getSquare(5).getColor());

            clone.bottom.getSquare(2).setColor(clone.left.getSquare(8).getColor());



            clone.left.getSquare(2).setColor(clone.holder.getSquare(8).getColor());

            clone.left.getSquare(5).setColor(clone.holder.getSquare(7).getColor());

            clone.left.getSquare(8).setColor(clone.holder.getSquare(6).getColor());



            clone.solvePath.Add(9);
            */

            clone.rotateFrontCounterClockwise();


            return clone;

        }



        /* 

        * Shuffles the back/blue Side of the Cube object in a clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public Cube createBackClockwise()
        {

            Cube clone = new Cube(this);


            /*
            for (int i = 0; i < 3; i++)
            {

                clone.holder.getSquare(i).setColor(clone.top.getSquare(i).getColor());

            }



            clone.top.getSquare(0).setColor(clone.right.getSquare(2).getColor());

            clone.top.getSquare(1).setColor(clone.right.getSquare(5).getColor());

            clone.top.getSquare(2).setColor(clone.right.getSquare(8).getColor());



            clone.right.getSquare(2).setColor(clone.bottom.getSquare(8).getColor());

            clone.right.getSquare(5).setColor(clone.bottom.getSquare(7).getColor());

            clone.right.getSquare(8).setColor(clone.bottom.getSquare(6).getColor());



            clone.bottom.getSquare(6).setColor(clone.left.getSquare(0).getColor());

            clone.bottom.getSquare(7).setColor(clone.left.getSquare(3).getColor());

            clone.bottom.getSquare(8).setColor(clone.left.getSquare(6).getColor());



            clone.left.getSquare(0).setColor(clone.holder.getSquare(2).getColor());

            clone.left.getSquare(3).setColor(clone.holder.getSquare(1).getColor());

            clone.left.getSquare(6).setColor(clone.holder.getSquare(0).getColor());



            clone.solvePath.Add(10);
            */

            clone.rotateBackClockwise();


            return clone;

        }



        /* 

        * Shuffles the back/blue Side of the Cube object in a counter clockwise direction.

        * Adds the corresponding path number to the Cube's solvePath list when finished.

        */

        public Cube createBackCounterClockwise()
        {

            Cube clone = new Cube(this);


            /*
            for (int i = 0; i < 3; i++)
            {

                clone.holder.getSquare(i).setColor(clone.top.getSquare(i).getColor());

            }



            clone.top.getSquare(0).setColor(clone.left.getSquare(6).getColor());

            clone.top.getSquare(1).setColor(clone.left.getSquare(3).getColor());

            clone.top.getSquare(2).setColor(clone.left.getSquare(0).getColor());



            clone.left.getSquare(0).setColor(clone.bottom.getSquare(6).getColor());

            clone.left.getSquare(3).setColor(clone.bottom.getSquare(7).getColor());

            clone.left.getSquare(6).setColor(clone.bottom.getSquare(8).getColor());



            clone.bottom.getSquare(6).setColor(clone.right.getSquare(8).getColor());

            clone.bottom.getSquare(7).setColor(clone.right.getSquare(5).getColor());

            clone.bottom.getSquare(8).setColor(clone.right.getSquare(2).getColor());



            clone.right.getSquare(2).setColor(clone.holder.getSquare(0).getColor());

            clone.right.getSquare(5).setColor(clone.holder.getSquare(1).getColor());

            clone.right.getSquare(8).setColor(clone.holder.getSquare(2).getColor());



            clone.solvePath.Add(11);
            */

            clone.rotateBackCounterClockwise();


            return clone;

        }

        /*

        * Returns the Cube's shufflePath list.

        */

        public List<int> getShufflePath()
        {

            return this.shufflePath;

        }



        /* 

        * Returns the Cube's solvePath list.

        */

        public List<int> getSolvePath()
        {

            return this.solvePath;

        }



        /* 

        * Returns a String output of a Cube object, formatted as:

        * <Name of Side>: 

        * <Color Number> | <Color Number> | <Color Number>

        * ------------------------------------------------

        * <Color Number> | <Color Number> | <Color Number> 

        * ------------------------------------------------

        * <Color Number> | <Color Number> | <Color Number> 

        */

        public override String ToString()
        {

            StringBuilder sb = new StringBuilder();



            sb.Append("Top: \n");

            sb.Append(top.ToString());

            sb.Append("\n");

            sb.Append("Left: \n");

            sb.Append(left.ToString());

            sb.Append("\n");

            sb.Append("Front: \n");

            sb.Append(front.ToString());

            sb.Append("\n");

            sb.Append("Right: \n");

            sb.Append(right.ToString());

            sb.Append("\n");

            sb.Append("Back: \n");

            sb.Append(back.ToString());

            sb.Append("\n");

            sb.Append("Bottom: \n");

            sb.Append(bottom.ToString());



            return sb.ToString();

        }

    }



    class Side
    {

        private Square[] face = new Square[9];



        /*

        * Creates a new Side object, setting the Square color to black.

        */

        public Side()
        {

            for (int i = 0; i < face.Length; i++)
            {

                face[i] = new Square();

            }

        }



        /*

        * Creates a new Side object, setting the Square color to the entered <colorNumber> value.

        * 

        * @param colorNumber: The color number of the Side's face

        */

        public Side(int c)
        {

            for (int i = 0; i < face.Length; i++)
            {

                face[i] = new Square(c);

            }

        }



        /*

        * Returns a single Square of the Side, where <pos> corresponds to the postion of the squares:

        * 0 | 1 | 2

        * ---------

        * 3 | 4 | 5

        * ---------

        * 6 | 7 | 8

        *

        * @param pos: The position number of Square in the face array

        */

        public Square getSquare(int pos)
        {

            return this.face[pos];

        }



        /*

        * Returns a String output of a Side object, formatted as:

        * <Name of Side>: 

        * <Color Number> | <Color Number> | <Color Number>

        * ------------------------------------------------

        * <Color Number> | <Color Number> | <Color Number> 

        * ------------------------------------------------

        * <Color Number> | <Color Number> | <Color Number>

        */

        public override String ToString()
        {

            StringBuilder sb = new StringBuilder();



            for (int i = 0; i < 3; i++)
            {

                sb.Append(face[i].getColor());

                if (i != 2)
                {

                    sb.Append(" | ");

                }

            }

            sb.Append("\n");

            sb.Append("---------");

            sb.Append("\n");



            for (int i = 3; i < 6; i++)
            {

                sb.Append(face[i].getColor());

                if (i != 5)
                {

                    sb.Append(" | ");

                }

            }

            sb.Append("\n");

            sb.Append("---------");

            sb.Append("\n");



            for (int i = 6; i < 9; i++)
            {

                sb.Append(face[i].getColor());

                if (i != 8)
                {

                    sb.Append(" | ");

                }

            }

            sb.Append("\n");



            return sb.ToString();

        }

    }



    class Square
    {



        private int color;



        public Square()
        {

            this.color = 0;

        }

        public Square(int c)
        {

            this.color = c;

        }

        public void setColor(int c)
        {

            this.color = c;

        }

        public int getColor()
        {

            return this.color;

        }

    }



    class DepthFirst
    {

        public static String DepthFirstSearch(int depth, int depthmax, Cube c, String currPath, String result)

        {



            if (depth + 1 < depthmax)

            {



                if (WhiteCross(c) == 4)

                {

                    if ((currPath.Length < result.Length) || result == "")

                    { result = currPath; }

                }

                else

                {



                    DepthFirstSearch(depth + 1, depthmax, c.createLeftClockwise(), currPath + "cwl ", result);

                    DepthFirstSearch(depth + 1, depthmax, c.createLeftCounterClockwise(), currPath + "ccl ", result);

                    DepthFirstSearch(depth + 1, depthmax, c.createRightClockwise(), currPath + "cwr ", result);

                    DepthFirstSearch(depth + 1, depthmax, c.createRightCounterClockwise(), currPath + "ccr ", result);

                    DepthFirstSearch(depth + 1, depthmax, c.createTopClockwise(), currPath + "cwt ", result);

                    DepthFirstSearch(depth + 1, depthmax, c.createTopCounterClockwise(), currPath + "cct ", result);

                    DepthFirstSearch(depth + 1, depthmax, c.createBottomClockwise(), currPath + "cwbt ", result);

                    DepthFirstSearch(depth + 1, depthmax, c.createBottomCounterClockwise(), currPath + "ccbt ", result);

                    DepthFirstSearch(depth + 1, depthmax, c.createFrontClockwise(), currPath + "cwf ", result);

                    DepthFirstSearch(depth + 1, depthmax, c.createFrontCounterClockwise(), currPath + "ccf ", result);

                    DepthFirstSearch(depth + 1, depthmax, c.createBackClockwise(), currPath + "cwb ", result);

                    DepthFirstSearch(depth + 1, depthmax, c.createBackCounterClockwise(), currPath + "ccb ", result);

                }

            }

            return result;

        }

        public static Cube Greedy(int depth, int depthmax, List<Cube> explored, List<Cube> frontier)

        {

            Cube c = null;

            if (frontier.Count == 0)

            { c = explored[0]; }

            else

            {

                c = ExploreNext(explored, frontier);

                explored.Add(c);

            }

            if (depth + 1 < depthmax)

            {



                if (Heursitic(c) == 54)

                { return c; }



                else

                {

                    explored.Add(c);

                    List<Cube> tempList = new List<Cube>();



                    tempList.Add(c.createFrontClockwise());

                    tempList.Add(c.createFrontCounterClockwise());

                    tempList.Add(c.createBackClockwise());

                    tempList.Add(c.createBackCounterClockwise());

                    tempList.Add(c.createBackCounterClockwise());

                    tempList.Add(c.createLeftClockwise());

                    tempList.Add(c.createLeftCounterClockwise());

                    tempList.Add(c.createRightClockwise());

                    tempList.Add(c.createRightCounterClockwise());

                    tempList.Add(c.createTopClockwise());

                    tempList.Add(c.createTopCounterClockwise());

                    tempList.Add(c.createBottomClockwise());

                    tempList.Add(c.createBottomCounterClockwise());



                    foreach (Cube cube in tempList)

                    {

                        if (!OnList(cube, explored))

                        { frontier.Add(cube); }

                    }

                    return Greedy(depth + 1, depthmax, explored, frontier);









                    /*

                    switch (ExploreNext(c,lst))

                    {

                        case 1:

                            lst.Add(c.createFrontClockwise());

                            return "fcw " + Greedy(depth + 1, depthmax, c.createFrontClockwise(), lst);

                            break;

                        case -1:

                            lst.Add(c.createFrontCounterClockwise());

                            return "fcc " + Greedy(depth + 1, depthmax, c.createFrontCounterClockwise(), lst);

                            break;

                        case 2:

                            lst.Add(c.createBackClockwise());

                            return "bww " + Greedy(depth + 1, depthmax, c.createBackClockwise(), lst);

                            break;

                        case -2:

                            lst.Add(c.createBackCounterClockwise());

                            return "bcc " + Greedy(depth + 1, depthmax, c.createBackCounterClockwise(), lst);

                            break;

                        case 3:

                            lst.Add(c.createLeftClockwise());

                            return "lcw " + Greedy(depth + 1, depthmax, c.createLeftClockwise(), lst);

                            break;

                        case -3:

                            lst.Add(c.createLeftCounterClockwise());

                            return "lcc " + Greedy(depth + 1, depthmax, c.createLeftCounterClockwise(), lst);

                            break;

                        case 4:

                            lst.Add(c.createRightClockwise());

                            return "rcw " + Greedy(depth + 1, depthmax, c.createRightClockwise(), lst);

                            break;

                        case -4:

                            lst.Add(c.createRightCounterClockwise());

                            return "rcc " + Greedy(depth + 1, depthmax, c.createRightCounterClockwise(), lst);

                            break;

                        case 5:

                            lst.Add(c.createTopClockwise());

                            return "tcw " + Greedy(depth + 1, depthmax, c.createTopClockwise(), lst);

                            break;

                        case -5:

                            lst.Add(c.createTopCounterClockwise());

                            return "tcc " + Greedy(depth + 1, depthmax, c.createTopCounterClockwise(), lst);

                            break;

                        case 6:

                            lst.Add(c.createBottomClockwise());

                            return "bcw " + Greedy(depth + 1, depthmax, c.createBottomClockwise(), lst);

                            break;

                        case -6:

                            lst.Add(c.createBottomCounterClockwise());

                            return "bcc " + Greedy(depth + 1, depthmax, c.createBottomCounterClockwise(), lst);

                            break;

                        default:

                            return " error";

                            break;

                    }

                    */

                }

            }

            else

                Console.WriteLine("depthMax hit");

            return c;









        }





        public static int Heursitic(Cube c)

        {

           


            int squaresCorrect = 0;

            int j;

            for (j = 0; j < 9; j++)

            {



                if (c.front.getSquare(j).getColor() == 1)

                { squaresCorrect = squaresCorrect + 1; }

                if (c.back.getSquare(j).getColor() == 2)

                { squaresCorrect = squaresCorrect + 1; }

                if (c.left.getSquare(j).getColor() == 3)

                { squaresCorrect = squaresCorrect + 1; }

                if (c.right.getSquare(j).getColor() == 4)

                { squaresCorrect = squaresCorrect + 1; }

                if (c.top.getSquare(j).getColor() == 5)

                { squaresCorrect = squaresCorrect + 1; }

                if (c.bottom.getSquare(j).getColor() == 6)

                { squaresCorrect = squaresCorrect + 1; }



            }

            return squaresCorrect;



        }





        public static bool Compare(Cube c, Cube d)

        {

            bool same = true;

            int j;

            for (j = 0; j < 9; j++)

            {

                if (c.front.getSquare(j).getColor() != d.front.getSquare(j).getColor())

                { same = false; break; }

                else if (c.back.getSquare(j).getColor() != d.back.getSquare(j).getColor())

                { same = false; break; }

                else if (c.left.getSquare(j).getColor() != d.left.getSquare(j).getColor())

                { same = false; break; }

                else if (c.right.getSquare(j).getColor() != d.right.getSquare(j).getColor())

                { same = false; break; }

                else if (c.top.getSquare(j).getColor() != d.top.getSquare(j).getColor())

                { same = false; break; }

                else if (c.bottom.getSquare(j).getColor() != d.bottom.getSquare(j).getColor())

                { same = false; break; }

            }

            return same;

        }



        public static bool OnList(Cube c, List<Cube> lst)

        {

            bool onList = false;

            foreach (Cube cube in lst)

            {

                if (Compare(cube, c))

                {

                    onList = true;

                    break;

                }



            }

            return onList;

        }



        public static Cube ExploreNext(List<Cube> explored, List<Cube> frontier)

        {

            Cube returnCube = null;

            int largestNum = 0;

            foreach (Cube cube in frontier)

            {

                if (Heursitic(cube) > largestNum && !(OnList(cube, explored)))

                {

                    largestNum = Heursitic(cube);

                    returnCube = cube;

                }

            }

            return returnCube;

        }

        /*

        public static int ExploreNext(Cube c, List<Cube> lst)

        {

            int rotation = 0;

            int largestNum = 0;





            if (Heursitic(c.createFrontClockwise()) > largestNum && !(OnList(c.createFrontClockwise(),lst)))

            {

                largestNum = Heursitic(c.createFrontClockwise());

                rotation = 1;

            }

            if (Heursitic(c.createFrontCounterClockwise()) > largestNum && !(OnList(c.createFrontCounterClockwise(), lst)))

            {

                largestNum = Heursitic(c.createFrontCounterClockwise());

                rotation = -1;

            }

            if (Heursitic(c.createBackClockwise()) > largestNum && !(OnList(c.createBackClockwise(), lst)))

            {

                largestNum = Heursitic(c.createBackClockwise());

                rotation = 2;

            }

            if (Heursitic(c.createBackCounterClockwise()) > largestNum && !(OnList(c.createBackCounterClockwise(), lst)))

            {

                largestNum = Heursitic(c.createBackCounterClockwise());

                rotation = -2;

            }

            if (Heursitic(c.createLeftClockwise()) > largestNum && !(OnList(c.createLeftClockwise(), lst)))

            {

                largestNum = Heursitic(c.createLeftClockwise());

                rotation = 3;

            }

            if (Heursitic(c.createLeftCounterClockwise()) > largestNum && !(OnList(c.createLeftCounterClockwise(), lst)))

            {

                largestNum = Heursitic(c.createLeftCounterClockwise());

                rotation = -3;

            }

            if (Heursitic(c.createRightClockwise()) > largestNum && !(OnList(c.createRightClockwise(), lst)))

            {

                largestNum = Heursitic(c.createRightClockwise());

                rotation = 4;

            }

            if (Heursitic(c.createRightCounterClockwise()) > largestNum && !(OnList(c.createRightCounterClockwise(), lst)))

            {

                largestNum = Heursitic(c.createRightCounterClockwise());

                rotation = -4;

            }

            if (Heursitic(c.createTopClockwise()) > largestNum && !(OnList(c.createTopClockwise(), lst)))

            {

                largestNum = Heursitic(c.createTopClockwise());

                rotation = 5;

            }

            if (Heursitic(c.createTopCounterClockwise()) > largestNum && !(OnList(c.createTopCounterClockwise(), lst)))

            {

                largestNum = Heursitic(c.createTopCounterClockwise());

                rotation = -5;

            }

            if (Heursitic(c.createBottomClockwise()) > largestNum && !(OnList(c.createBottomClockwise(), lst)))

            {

                largestNum = Heursitic(c.createBottomClockwise());

                rotation = 6;

            }

            if (Heursitic(c.createBottomCounterClockwise()) > largestNum && !(OnList(c.createBottomCounterClockwise(), lst)))

            {

                largestNum = Heursitic(c.createBottomCounterClockwise());

                rotation = -6;

            }

            return rotation;







        }

        */

        //Color Map Reference

        //Black  : Blank  : 0

        //Green  : Front  : 1

        //Blue   : Back   : 2

        //Orange : Left   : 3

        //Red    : Right  : 4

        //White  : Top    : 5

        //Yellow : Bottom : 6


        static public int WhiteCross(Cube c)
        {
            int crossCt = 0;
            if (c.top.getSquare(1).getColor() == 5)
            { crossCt++; }
            if (c.top.getSquare(3).getColor() == 5)
            { crossCt++; }
            if (c.top.getSquare(5).getColor() == 5)
            { crossCt++; }
            if (c.top.getSquare(7).getColor() == 5)
            { crossCt++; }

            //if (c.top.getSquare(0).getColor() == 5 && c.back.getSquare(2).getColor() == 2)
            //{ crossCt++; }
            //if (c.top.getSquare(2).getColor() == 5 && c.right.getSquare(2).getColor() == 4)
            //{ crossCt++; }
            //if (c.top.getSquare(6).getColor() == 5 && c.left.getSquare(2).getColor() == 3)
            //{ crossCt++; }
            //if (c.top.getSquare(8).getColor() == 5 && c.front.getSquare(2).getColor() == 1)
            //{ crossCt++; }
            return crossCt;
        }

        static public Cube greedyCross(List<Cube> explored, List<Cube> frontier)
        {
            Cube c = null;
            if (frontier.Count == 0)
            { c = explored[0];
                c.depth = 0;  
            }

            else
            {

                int largestNum = (WhiteCross(frontier[0]) - frontier[0].getSolvePath().Count) - 1;
                //Cube tempCube;
                foreach (Cube cube in frontier)
                {
                    if ((WhiteCross(cube) - cube.getSolvePath().Count) > largestNum)
                    {
                        largestNum = WhiteCross(cube) - cube.getSolvePath().Count;
                        c = cube;
                    }
                }

                frontier.Remove(c);

                }
            
            List<Cube> tempList = new List<Cube>();

            tempList.Add(c.createFrontClockwise());

            tempList.Add(c.createFrontCounterClockwise());

            tempList.Add(c.createBackClockwise());

            tempList.Add(c.createBackCounterClockwise());

            tempList.Add(c.createBackCounterClockwise());

            tempList.Add(c.createLeftClockwise());

            tempList.Add(c.createLeftCounterClockwise());

            tempList.Add(c.createRightClockwise());

            tempList.Add(c.createRightCounterClockwise());

            tempList.Add(c.createTopClockwise());

            tempList.Add(c.createTopCounterClockwise());

            tempList.Add(c.createBottomClockwise());

            tempList.Add(c.createBottomCounterClockwise());



            foreach (Cube cube in tempList)

            {

                if (!OnList(cube, explored))

                { frontier.Add(cube); }

            }
            if (WhiteCross(c) == 4) return c;
            int help = WhiteCross(c);
            help = help + 0; 
            return greedyCross(explored, frontier);



        }

        public static Cube WhiteCorner(Cube c)
        {
            int location = 0;
            


            int count = 0;
            int temp = 0;
            while(c.top.getSquare(8).getColor() != 5 && count == 0)
            {
                c.rotateRightCounterClockwise();
                c.rotateBottomCounterClockwise();
                c.rotateRightClockwise();
                c.rotateBottomClockwise();
                temp++;
                if (temp == 6)
                {
                    temp = 0;
                    c.rotateBottomClockwise();
                }

            }
            count++;
            temp = 0;
            while(c.top.getSquare(6).getColor() != 5 && count == 1)
            {
                c.rotateFrontCounterClockwise();
                c.rotateBottomCounterClockwise();
                c.rotateFrontClockwise();
                c.rotateBottomClockwise();
                temp++;
                if (temp == 6)
                {
                    temp = 0;
                    c.rotateBottomClockwise();
                }
            }
            count++;
            temp = 0;
            while (c.top.getSquare(0).getColor() != 5 && count == 2)
            {
                c.rotateLeftCounterClockwise();
                c.rotateBottomCounterClockwise();
                c.rotateLeftClockwise();
                c.rotateBottomClockwise();
                temp++;
                if (temp == 6)
                {
                    temp = 0;
                    c.rotateBottomClockwise();
                }
            }
            count++;
            temp = 0;
            while (c.top.getSquare(2).getColor() != 5 && count == 3)
            {
                c.rotateBackCounterClockwise();
                c.rotateBottomCounterClockwise();
                c.rotateBackClockwise();
                c.rotateBottomClockwise();
                temp++;
                if (temp == 6)
                {
                    temp = 0;
                    c.rotateBottomClockwise();
                }
            }


            return c;
            



        }
    }


}


