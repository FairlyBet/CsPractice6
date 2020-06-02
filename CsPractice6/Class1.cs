using System;
using System.Threading;

namespace CsPractice6
{
    class TeamFight
    {
        int firstTeam;
        int secondTeam;
        int round;
        readonly object locker = new object();
        readonly Random random;

        public TeamFight()
        {
            random = new Random();
            firstTeam = random.Next(400) + 101;
            secondTeam = random.Next(400) + 101;
            round = 1;
        }

        public void FirstTeam()
        {
            while (true)
            {
                lock (locker)
                {
                    if (firstTeam > 0 && secondTeam > 0)
                    {
                        firstTeam += random.Next(500) + 1;
                        secondTeam -= random.Next(500) + 1;
                        if (secondTeam < 0)
                            secondTeam = 0;
                        Console.WriteLine($"Round {round++}\nfirst team: {firstTeam}\nsecond team {secondTeam}\n");
                        Thread.Sleep(500);
                    }
                    else break;
                }
            }
        }

        public void SecondTeam()
        {
            while (true)
            {
                lock (locker)
                {
                    if (firstTeam > 0 && secondTeam > 0)
                    {
                        secondTeam += random.Next(500) + 1;
                        firstTeam -= random.Next(500) + 1;
                        if (firstTeam < 0)
                            firstTeam = 0;
                        Console.WriteLine($"Round {round++}\nsecond team {secondTeam}\nfirst team: {firstTeam}\n");
                        Thread.Sleep(500);
                    }
                    else break;
                }
            }
        }

        public void StartBattle()
        {
            Console.WriteLine($"Begin: first team: {firstTeam} second team: {secondTeam}\n");
            Thread firstTeamThread = new Thread(new ThreadStart(FirstTeam));
            Thread secondTeamThread = new Thread(new ThreadStart(SecondTeam));
            firstTeamThread.Start();
            secondTeamThread.Start();
            while (true)
            {
                if (firstTeamThread.ThreadState == ThreadState.Stopped && secondTeamThread.ThreadState == ThreadState.Stopped)
                {
                    Console.WriteLine(this.ToString());
                    break;
                }              
            }
        }

        public override string ToString()
        {
            string result = "";
            if (firstTeam == 0)
                result += "Winner - second team: " + secondTeam + "\nLoser - first team: 0";
            else
                result += "Winner - first team: " + firstTeam + "\nLoser - second team: 0";
            return result;
        }
    }
}