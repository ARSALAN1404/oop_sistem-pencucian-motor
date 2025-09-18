using System;
using System.Collections.Generic;

// 1. Class & Object: Class Motor
public class Motor
{
    // Encapsulation & Data Hiding: Properti private
    private string _merk;
    private string _model;
    private string _platNomor;
    private bool _isBersih;

    // Public Properties untuk mengakses data
    public string Merk
    {
        get { return _merk; }
        private set { _merk = value; } // Set bisa private jika ingin kontrol lebih
    }

    public string Model
    {
        get { return _model; }
        private set { _model = value; }
    }

    public string PlatNomor
    {
        get { return _platNomor; }
        private set { _platNomor = value; }
    }

    public bool IsBersih
    {
        get { return _isBersih; }
        set { _isBersih = value; } // Setter publik karena status kebersihan bisa berubah
    }

    // 2. Constructor
    public Motor(string merk, string model, string platNomor)
    {
        Merk = merk;
        Model = model;
        PlatNomor = platNomor;
        IsBersih = false; // Motor awalnya kotor
        Console.WriteLine($"Motor {Merk} {Model} dengan plat nomor {PlatNomor} telah didaftarkan.");
    }

    public void TampilkanStatus()
    {
        Console.WriteLine($"\n--- Detail Motor ---");
        Console.WriteLine($"Merk: {Merk}");
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"Plat Nomor: {PlatNomor}");
        Console.WriteLine($"Status Kebersihan: {(IsBersih ? "Bersih" : "Kotor")}");
    }
}

// 1. Class & Object: Class PencucianMotor
public class PencucianMotor
{
    private string _namaPencucian;
    private List<Motor> _antrianCuci; // 6. Composition: Memiliki list Motor

    public string NamaPencucian
    {
        get { return _namaPencucian; }
        private set { _namaPencucian = value; }
    }

    public PencucianMotor(string nama)
    {
        NamaPencucian = nama;
        _antrianCuci = new List<Motor>();
        Console.WriteLine($"\nSistem pencucian '{NamaPencucian}' telah dimulai.");
    }

    public void TambahAntrian(Motor motor)
    {
        _antrianCuci.Add(motor);
        Console.WriteLine($"Motor {motor.Merk} {motor.Model} ({motor.PlatNomor}) telah ditambahkan ke antrian.");
    }

    // 5. Polymorphism (Overloading): Method Cuci tanpa parameter khusus
    public void Cuci(Motor motor)
    {
        Console.WriteLine($"\nSedang mencuci motor {motor.Merk} {motor.Model} ({motor.PlatNomor})...");
        System.Threading.Thread.Sleep(2000); // Simulasi proses cuci
        motor.IsBersih = true;
        Console.WriteLine($"Motor {motor.Merk} {motor.Model} ({motor.PlatNomor}) sekarang BERSIH!");
    }

    // 5. Polymorphism (Overloading): Method Cuci dengan parameter jenis cuci
    public void Cuci(Motor motor, string jenisCuci)
    {
        Console.WriteLine($"\nSedang melakukan cuci {jenisCuci} untuk motor {motor.Merk} {motor.Model} ({motor.PlatNomor})...");
        System.Threading.Thread.Sleep(3000); // Simulasi proses cuci lebih lama
        motor.IsBersih = true;
        Console.WriteLine($"Motor {motor.Merk} {motor.Model} ({motor.PlatNomor}) sekarang BERSIH dengan cuci {jenisCuci}!");
    }

    public void ProsesAntrian()
    {
        if (_antrianCuci.Count == 0)
        {
            Console.WriteLine("\nTidak ada motor dalam antrian.");
            return;
        }

        Console.WriteLine($"\n--- Memulai Proses Antrian di {NamaPencucian} ---");
        foreach (var motor in _antrianCuci)
        {
            if (!motor.IsBersih)
            {
                Cuci(motor); // Memanggil method Cuci default
            }
            else
            {
                Console.WriteLine($"Motor {motor.Merk} {motor.Model} ({motor.PlatNomor}) sudah bersih, tidak perlu dicuci lagi.");
            }
        }
        _antrianCuci.Clear(); // Hapus antrian setelah selesai diproses
        Console.WriteLine("--- Proses Antrian Selesai ---");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Membuat objek Motor
        Motor motorA = new Motor("Honda", "Vario 150", "B 1234 ABC");
        Motor motorB = new Motor("Yamaha", "NMAX", "D 5678 EFG");
        Motor motorC = new Motor("Kawasaki", "Ninja 250", "F 9012 HIJ");

        // Menampilkan status awal motor
        motorA.TampilkanStatus();
        motorB.TampilkanStatus();

        // Membuat objek PencucianMotor
        PencucianMotor sistemCuci = new PencucianMotor("Berkah Jaya Motor Wash");

        // Menambahkan motor ke antrian
        sistemCuci.TambahAntrian(motorA);
        sistemCuci.TambahAntrian(motorB);
        sistemCuci.TambahAntrian(motorC);

        // Memproses antrian
        sistemCuci.ProsesAntrian();

        // Menampilkan status motor setelah dicuci
        motorA.TampilkanStatus();
        motorB.TampilkanStatus();
        motorC.TampilkanStatus();

        // Contoh penggunaan overloading
        Console.WriteLine("\n--- Contoh Overloading ---");
        Motor motorD = new Motor("Suzuki", "Satria F150", "A 4321 KLM");
        sistemCuci.Cuci(motorD, "Cuci Premium"); // Memanggil method Cuci yang overloaded
        motorD.TampilkanStatus();

        Console.ReadKey(); // Agar console tidak langsung tertutup
    }
}