using Npgsql;

const string connectionString = "Host=localhost;Username=postgres;Password=yourpassword;Database=postgres";

await using var connection = new NpgsqlConnection(connectionString);
await connection.OpenAsync();

await using (var command = new NpgsqlCommand("insert into table_test (column_one) values (@column_one)", connection))
{
    command.Parameters.AddWithValue("column_one", "GREEN LEAFS");
    await command.ExecuteNonQueryAsync();
}

await using (var command = new NpgsqlCommand("select column_one from table_test", connection))
{
    await using (var reader = await command.ExecuteReaderAsync())
    {
        while (await reader.ReadAsync())
        {
            Console.WriteLine(reader.GetString(0));
        }
    }
}