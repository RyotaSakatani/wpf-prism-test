using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public interface ICustomerRepository
{
    IEnumerable<Customer> GetAll();
}

public class CsvCustomerRepository : ICustomerRepository
{
    private readonly string _csvPath;

    public CsvCustomerRepository(string csvPath)
    {
        _csvPath = csvPath;
    }

    public IEnumerable<Customer> GetAll()
    {
        var customers = new List<Customer>();

        if (!File.Exists(_csvPath))
        {
            // ファイルがなければ空リストを返す
            return customers;
        }

        foreach (var line in File.ReadLines(_csvPath))
        {
            // ヘッダ行はスキップする
            if (line.StartsWith("Id")) continue;

            var parts = line.Split(',');
            if (parts.Length < 3) continue; // 項目数異常ならNG

            if (int.TryParse(parts[0], out int id))
            {
                DateTime.TryParse(parts[2], out DateTime birthday);

                customers.Add(new Customer
                {
                    CustomerId = id,
                    Name = parts[1],
                    BirthDate = birthday
                });
            }
        }

        return customers;
    }
}