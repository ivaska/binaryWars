using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace UFOSoldier
{
    [Serializable]
    public class Solder:ISerializable
    { 
        public byte[] NameArray { get; set; } // как быть со строкой/массивом заданной длины
        public byte TimeUnit { get; set; }
        public byte Health { get; set; }
        public byte Stamina { get; set; }
        public byte Reaction{ get; set; }
        public byte Strength{ get; set; }
        public byte FiringAccuracy{ get; set; }
        public byte ThrowingAccuracy{ get; set; }
        public byte PsiStrength{ get; set; }
        public byte PsiSkill { get; set; }
        public byte Bravery{ get; set; }
       
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Width", this.TimeUnit);
           // info.AddValue("Length", this.Length);
           // info.AddValue("hasFurniture", this.HasFurniture);
        }
        public Solder(SerializationInfo info, StreamingContext context)
        {
            this.TimeUnit = (byte)info.GetValue("Width", typeof(byte));
            //this.Length = (double)info.GetValue("Length", typeof(double));
            //this.HasFurniture = (bool)info.GetValue("hasFurniture", typeof(bool));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            long Wounds1 = Convert.ToInt64("000C", 16);
            long Wounds2 = Convert.ToInt64("000D", 16);
            long Rank = Convert.ToInt64("0000", 16);
            long Suit = Convert.ToInt64("003E", 16);
            long TimeUnit = Convert.ToInt64("002A",16);//         1 Byte FF
            long Health = Convert.ToInt64("002B",16);//         1 Byte FF
            long Stamina = Convert.ToInt64("002C",16);//         1 Byte FF
            long Reaction = Convert.ToInt64("002D",16); //      1 Byte FF
            long Strength = Convert.ToInt64("002E",16);//         1 Byte         64
            long FiringAccuracy = Convert.ToInt64("002F",16); //       1 Byte FF
            long ThrowingAccuracy = Convert.ToInt64("0030",16); //      1 Byte FF
            long PsiStrength = Convert.ToInt64("0032",16); //    1 Byte FF
            long PsiSkill = Convert.ToInt64("0033",16); //   1 Byte FF
            long Bravery = Convert.ToInt64("0034",16); //   1 Byte         01
            long offSet = Convert.ToInt64("0044", 16);
            string path = @"c:\Program Files (x86)\GalaxyClient\Games\X-COM UFO Defense\GAME_9\soldier.dat";
            byte[] content = File.ReadAllBytes(path);
           
            //яя                                                                  
            for (int i = 0; i < 5; i++)
            {
                Console.Write("Upgrading...");
                //поиск по имени, пустое - не пустое
                //сериализация в бинарик, посмотреть
                //https://alekseygulynin.ru/binaryformatter-c/
                //длина имени и фамилии 26?
                //
                for (long j = 16 + offSet * i; j < 42 + offSet * i; j++)
                {
                    Console.Write((char)content[j]);
                }
                Console.WriteLine();
                content[Rank + i * offSet] = (byte)05;
                content[Suit + i * offSet] = (byte)03;
                content[Wounds1 + i * offSet] = (byte)0;
                content[Wounds2 + i * offSet] = (byte)0;
                content[TimeUnit + i * offSet] = (byte)180;
                content[Health + i * offSet] = (byte)180;
                content[Stamina + i * offSet] = (byte)180;
                content[Reaction + i * offSet] = (byte)180;
                content[Strength + i * offSet] = (byte)60;
                content[FiringAccuracy + i * offSet] = (byte)180;
                content[ThrowingAccuracy + i * offSet] = (byte)180;
                content[PsiSkill + i * offSet] = (byte)180;
                content[PsiStrength + i * offSet] = (byte)180;
                content[Bravery + i * offSet] = (byte)4; //             110 - Bravery
                                                         // Hex Value = -------------
                                                         //                 10

                //long ptr = Convert.ToInt64(Strength, 16);
                //Console.Write(content[ptr+i*offSet].ToString());

            }
            File.WriteAllBytes(path,content);



            Console.ReadKey();
        }
    }
}
