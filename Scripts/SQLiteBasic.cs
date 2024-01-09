using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite; 
using System.Data;

public class SQLiteBasic : MonoBehaviour
{
    public static SQLiteBasic Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField] private int hitCount = 0;

    void Start() 
    {
       
        IDbConnection dbConnection = CreateAndOpenDatabase(); 
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); 
        dbCommandReadValues.CommandText = "SELECT * FROM HIGHSCORETABLE"; 
        IDataReader dataReader = dbCommandReadValues.ExecuteReader(); 

        while (dataReader.Read()) 
        {
           
            hitCount = dataReader.GetInt32(1); 
        }

        
        dbConnection.Close(); 
    }


    public class PlayerData
    {
        string playerName;
        int score;
    }

    public int[] scores;

    public string testUsernameString;
    public int testScore;

    public int dataReceived;

    [ContextMenu("Send Example Value To DB")]
    public void SendExampleValueToDB()
    {
        SendUserScoreToDB(testUsernameString, testScore);
    }

    public void SendScore(string _userName, int score)
    {
        SendUserScoreToDB(_userName, score);
    }


    [ContextMenu("Send Multi Example Value To DB")]
    public void SendExampleMultiValue()
    {
        SendUserScoreToDB("playerName", Random.Range(0, 5));
        SendUserScoreToDB("playerName2", Random.Range(0, 35));
        SendUserScoreToDB("playerName3", Random.Range(0, 35));
        SendUserScoreToDB("playerName4", Random.Range(0, 25));
        SendUserScoreToDB("playerName5", Random.Range(0, 35));
    }

    private void SendUserScoreToDB(string kullaniciAdi, int _value)  
    {
     
        IDbConnection dbConnection = CreateAndOpenDatabase(); 
        IDbCommand dbCommandInsertValue = dbConnection.CreateCommand(); 

        dbCommandInsertValue.CommandText = "INSERT OR REPLACE INTO HIGHSCORETABLE (Username, Score) VALUES (@Username, @Score)"; // 10

      
        IDbDataParameter usernameParameter = dbCommandInsertValue.CreateParameter();
        usernameParameter.ParameterName = "@Username";
        usernameParameter.Value = kullaniciAdi;
        dbCommandInsertValue.Parameters.Add(usernameParameter);

        IDbDataParameter scoreParameter = dbCommandInsertValue.CreateParameter();
        scoreParameter.ParameterName = "@Score";
        scoreParameter.Value = _value;
        dbCommandInsertValue.Parameters.Add(scoreParameter);

        dbCommandInsertValue.ExecuteNonQuery(); 

       
        dbConnection.Close(); 
    }

    [ContextMenu("Get Data User")]
    public void GetData1()
    {
        dataReceived = GetScore(testUsernameString);
    }

    public int GetScore(string _userName)

    {
      
        IDbConnection dbConnection = CreateAndOpenDatabase(); 
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); 
        dbCommandReadValues.CommandText = $"SELECT Score FROM HIGHSCORETABLE WHERE Username = '{_userName}'"; 
        IDataReader dataReader = dbCommandReadValues.ExecuteReader(); 

        while (dataReader.Read()) 
        {
           
            int _score = dataReader.GetInt32(0); 
            dbConnection.Close(); 
            return _score;
        }

        
        dbConnection.Close(); 
        return 0;
    }

    private IDbConnection CreateAndOpenDatabase()
    {
       
        string dbUri = "URI=file:MyDatabase.sqlite"; 
        IDbConnection dbConnection = new SqliteConnection(dbUri); 
        dbConnection.Open(); 

        
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand();
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS HIGHSCORETABLE (Username STRING PRIMARY KEY, Score INTEGER )";
        dbCommandCreateTable.ExecuteReader(); 

        return dbConnection;
    }

    [ContextMenu("Send Example Highscore")]
    public void SendHighscoreExample()
    {
        string userName = testUsernameString;
        int _score = testScore;

        int _dbHighscore = GetScore(userName);

        Debug.Log("Previous Highscore : " + _dbHighscore.ToString());

        if (_score > _dbHighscore)
        {
            Debug.Log("Our new score : " + _score + " was higher than previous Highscore : " + _dbHighscore + " in the DB");
            
            SendUserScoreToDB(userName, _score);
        }
        else
        {
            Debug.Log("Our new score : " + _score + " was lower than previous Highscore : " + _dbHighscore + " in the DB, so we're not sending it");
        }

    }


    public void SendHighscore(string userName,int _score)
    {

        int _dbHighscore = GetScore(userName);

        Debug.Log("Previous Highscore : " + _dbHighscore.ToString());

        if (_score > _dbHighscore)
        {
            Debug.Log("Our new score : " + _score + " was higher than previous Highscore : " + _dbHighscore + " in the DB");
            
            SendUserScoreToDB(userName, _score);
        }
        else
        {
            Debug.Log("Our new score : " + _score + " was lower than previous Highscore : " + _dbHighscore + " in the DB, so we're not sending it");
        }

    }

}
