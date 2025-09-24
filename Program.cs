using System;                     // Library bawaan C# untuk fungsi umum (contoh: Console.WriteLine)
using System.Collections.Generic; // Library untuk struktur data generik (contoh: List<T>)

// =========================================
// 1. Class & Object
// =========================================
public class Motor
{
    // =========================================
    // 3. Encapsulation & Data Hiding
    // Field dibuat private agar tidak bisa diakses langsung dari luar class
    // =========================================
    private string _merk;
    private string _model;
    private string _platNomor;
    private bool _isBersih;

    // Public Properties -> cara aman untuk mengakses field private
    public string Merk
    {
        get { return _merk; }       // bisa dibaca
        private set { _merk = value; } // hanya bisa di-set dari dalam class
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
        set { _isBersih = value; }  // status kebersihan bisa diubah dari luar
    }

    // =========================================
    // 2. Constructor
    // Dipanggil otomatis ketika object baru dibuat
    // =========================================
    public Motor(string merk, string model, string platNomor)
    {
        Merk = merk;
        Model = model;
        PlatNomor = platNomor;
        IsBersih = false; // default: motor baru didaftarkan dalam kondisi kotor
        Console.WriteLine($"Motor {Merk} {Model} dengan plat nomor {PlatNomor} telah didaftarkan.");
    }

    // Virtual = method ini bisa di-override oleh class turunan
    public virtual void TampilkanStatus()
    {
        Console.WriteLine($"\n--- Detail Motor ---");
        Console.WriteLine($"Merk: {Merk}");
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"Plat Nomor: {PlatNomor}");
        Console.WriteLine($"Status Kebersihan: {(IsBersih ? "Bersih" : "Kotor")}");
    }
}

// =========================================
// 4. Inheritance
// Class MotorSport adalah turunan dari class Motor
// =========================================
public class MotorSport : Motor
{
    // Constructor MotorSport memanggil constructor dari class Motor (base)
    public MotorSport(string merk, string model, string platNomor)
        : base(merk, model, platNomor) { }

    // =========================================
    // 5. Polymorphism (Overriding)
    // Method TampilkanStatus diubah tampilannya khusus untuk MotorSport
    // =========================================
    public override void TampilkanStatus()
    {
        Console.WriteLine($"\n--- Detail MotorSport ---");
        Console.WriteLine($"Merk: {Merk}");
        Console.WriteLine($"Model: {Model} (Sport Edition)"); // tambahan label Sport Edition
        Console.WriteLine($"Plat Nomor: {PlatNomor}");
        Console.WriteLine($"Status Kebersihan: {(IsBersih ? "Bersih" : "Kotor")}");
    }

    // Method baru khusus untuk MotorSport
    public void NyalakanTurbo()
    {
        Console.WriteLine($"{Merk} {Model} mengaktifkan TURBO!");
    }
}

// =========================================
// Class lain dengan Composition
// PencucianMotor memiliki daftar Motor (List<Motor>)
// =========================================
public class PencucianMotor
{
    private string _namaPencucian;
    private List<Motor> _antrianCuci; // 6. Composition: class ini menyimpan list motor

    public string NamaPencucian
    {
        get { return _namaPencucian; }
        private set { _namaPencucian = value; }
    }

    // Constructor PencucianMotor
    public PencucianMotor(string nama)
    {
        NamaPencucian = nama;
        _antrianCuci = new List<Motor>(); // inisialisasi list kosong
        Console.WriteLine($"\nSistem pencucian '{NamaPencucian}' telah dimulai.");
    }

    // Menambahkan motor ke antrian cuci
    public void TambahAntrian(Motor motor)
    {
        _antrianCuci.Add(motor);
        Console.WriteLine($"Motor {motor.Merk} {motor.Model} ({motor.PlatNomor}) telah ditambahkan ke antrian.");
    }

    // 5. Polymorphism (Overloading)
    // Method Cuci() versi pertama (tanpa parameter tambahan)
    public void Cuci(Motor motor)
    {
        Console.WriteLine($"\nSedang mencuci motor {motor.Merk} {motor.Model} ({motor.PlatNomor})...");
        System.Threading.Thread.Sleep(2000); // simulasi delay proses cuci
        motor.IsBersih = true;
        Console.WriteLine($"Motor {motor.Merk} {motor.Model} ({motor.PlatNomor}) sekarang BERSIH!");
    }

    // 5. Polymorphism (Overloading)
    // Method Cuci() versi kedua (dengan parameter jenis cuci)
    public void Cuci(Motor motor, string jenisCuci)
    {
        Console.WriteLine($"\nSedang melakukan cuci {jenisCuci} untuk motor {motor.Merk} {motor.Model} ({motor.PlatNomor})...");
        System.Threading.Thread.Sleep(3000); // simulasi proses lebih lama
        motor.IsBersih = true;
        Console.WriteLine($"Motor {motor.Merk} {motor.Model} ({motor.PlatNomor}) sekarang BERSIH dengan cuci {jenisCuci}!");
    }

    // Memproses semua motor dalam antrian
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
            if (!motor.IsBersih) // kalau masih kotor
            {
                Cuci(motor);
            }
            else
            {
                Console.WriteLine($"Motor {motor.Merk} {motor.Model} ({motor.PlatNomor}) sudah bersih, tidak perlu dicuci lagi.");
            }
        }
        _antrianCuci.Clear(); // kosongkan antrian setelah selesai
        Console.WriteLine("--- Proses Antrian Selesai ---");
    }
}

// =========================================
// Program Utama
// =========================================
public class Program
{
    public static void Main(string[] args)
    {
        // Membuat beberapa object motor
        Motor motorA = new Motor("Honda", "Vario 150", "B 1234 ABC");
        Motor motorB = new Motor("Yamaha", "NMAX", "D 5678 EFG");
        Motor motorC = new Motor("Kawasaki", "Ninja 250", "F 9012 HIJ");

        // Menampilkan status awal motor
        motorA.TampilkanStatus();
        motorB.TampilkanStatus();

        // Membuat object PencucianMotor (composition)
        PencucianMotor sistemCuci = new PencucianMotor("Berkah Jaya Motor Wash");
        sistemCuci.TambahAntrian(motorA);
        sistemCuci.TambahAntrian(motorB);
        sistemCuci.TambahAntrian(motorC);

        // Proses semua motor dalam antrian
        sistemCuci.ProsesAntrian();

        // Menampilkan status setelah dicuci
        motorA.TampilkanStatus();
        motorB.TampilkanStatus();
        motorC.TampilkanStatus();

        // Contoh Polymorphism (Overloading)
        Console.WriteLine("\n--- Contoh Overloading ---");
        Motor motorD = new Motor("Suzuki", "Satria F150", "A 4321 KLM");
        sistemCuci.Cuci(motorD, "Cuci Premium"); // versi method dengan parameter tambahan
        motorD.TampilkanStatus();

        // Contoh Inheritance + Overriding
        Console.WriteLine("\n--- Contoh Inheritance & Overriding ---");
        MotorSport motorSport = new MotorSport("Ducati", "Panigale V4", "Z 7777 XYZ");
        motorSport.TampilkanStatus(); // method hasil override
        motorSport.NyalakanTurbo();   // method khusus MotorSport

        Console.ReadKey(); // agar console tidak langsung tertutup
    }
}
