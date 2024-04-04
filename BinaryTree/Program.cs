using System;

class Node
{
    public double Value { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(double chislo)
    {
        Value = chislo;
        Left = null;
        Right = null;
    }
}

class BinaryTree
{
    private Node root;
    private Random random;

    public BinaryTree()
    {
        root = null;
        random = new Random();
    }

    public void Generate() // генерация значений вершин бинарного дерева
    {
        int znach;
        while (true)
        {
            Console.WriteLine("Введите количество вершин бинарного дерева(не менее 12): ");
            if (int.TryParse(Console.ReadLine(), out znach) && znach >= 12)
            {
                break;
            }
            Console.WriteLine("Бинарное дерево должно содержать не менее 12 вершин. Введите значение заново!");
        }

        double lambda;
        while (true)
        {
            Console.WriteLine("Введите значение параметра lambda (от 0 до 1): ");
            if (double.TryParse(Console.ReadLine(), out lambda) && lambda > 0 && lambda <= 1)
            {
                break;
            }
            Console.WriteLine("Значение параметра lambda должно быть в диапазоне от 0 до 1. Введите значение заново!");
        }

        for (int i = 0; i < znach; i++)
        {
            double randomRValueOfR = GenerateExponentialRandomNumber(lambda);
            double chislo = (double)Math.Round(randomRValueOfR, 3); // 3 знака после запятой и преобразование типа
            Insert(chislo);
        }
    }

    private double GenerateExponentialRandomNumber(double lambda)//экспоненциальное распределение
    {
        double randomRValueOfR = random.NextDouble();
        double exponentialValue = -Math.Log(1 - randomRValueOfR) / lambda; // x = -(1/lambda) * ln(1-R) - общая формула генерации

        return exponentialValue;
    }


    public void Insert(double chislo)
    {
        if (root == null)
        {
            root = new Node(chislo);
        }
        else
        {
            VvodRec(root, chislo);
        }
    }

    private void VvodRec(Node currentNode, double chislo)//заполнение деерева значениями с проверкой значения вершины(для того чтобы строилось именно бинарное деерево)
    {
        if (chislo < currentNode.Value)
        {
            if (currentNode.Left == null)
            {
                currentNode.Left = new Node(chislo);
            }
            else
            {
                VvodRec(currentNode.Left, chislo);
            }
        }
        else
        {
            if (currentNode.Right == null)
            {
                currentNode.Right = new Node(chislo);
            }
            else
            {
                VvodRec(currentNode.Right, chislo);
            }
        }
    }
    private Node FindMinValueNode(Node node)
    {
        Node current = node;
        while (current.Left != null)
        {
            current = current.Left;
        }
        return current;
    }
    public void Delete(double chislo)
    {
        root = DeleteRec(root, chislo);
    }

    private Node DeleteRec(Node currentNode, double chislo)//метод для удаления вершины, значение которой вбивает пользователь
    {
        if (currentNode == null)
        {
            return null;
        }

        if (chislo < currentNode.Value)
        {
            currentNode.Left = DeleteRec(currentNode.Left, chislo);
        }
        else if (chislo > currentNode.Value)
        {
            currentNode.Right = DeleteRec(currentNode.Right, chislo);
        }
        else
        {
            if (currentNode.Left == null && currentNode.Right == null)
            {
                return null;
            }
            else if (currentNode.Left == null)
            {
                return currentNode.Right;
            }
            else if (currentNode.Right == null)
            {
                return currentNode.Left;
            }
            else
            {
                Node minValueNode = FindMinValueNode(currentNode.Right);
                currentNode.Value = minValueNode.Value;
                currentNode.Right = DeleteRec(currentNode.Right, minValueNode.Value);
            }
        }

        return currentNode;
    }

   

    public void Print()
    {
        PrintRec(root, "");
    }

    private void PrintRec(Node currentNode, string otstup, bool isLast = true)//метод для красивой отрисовки бинарного дерева
    {
        if (currentNode != null)
        {
            Console.Write(otstup);

            if (isLast)
            {
                Console.Write("└─");
                otstup += "  ";
            }
            else
            {
                Console.Write("├─");
                otstup += "│ ";
            }

            Console.WriteLine(currentNode.Value);

            PrintRec(currentNode.Left, otstup, currentNode.Right == null);
            PrintRec(currentNode.Right, otstup, true);
        }
    }

    
}

class Program
{
    static void Main(string[] args)
    {
        BinaryTree binaryTree = new BinaryTree();

        binaryTree.Generate();

        Console.WriteLine("Сгенерированное бинарное дерево:");
        binaryTree.Print();

        Console.Write("Введите значение вершины, которую вы хотите удалить: ");
        double chisloToDelete = double.Parse(Console.ReadLine());

        binaryTree.Delete(chisloToDelete);

        Console.WriteLine("\nБинарное дерево после удаления вершины:");
        binaryTree.Print();

        Console.ReadLine();
    }
}
