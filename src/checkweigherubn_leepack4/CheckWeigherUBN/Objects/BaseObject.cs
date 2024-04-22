using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckWeigherUBN
{
  public class BaseObject
  {

    public Dictionary<String, String> Dictionary = new Dictionary<String, String>();


    public BaseObject()
    {
    }



    public virtual Dictionary<String, String> BuildDictionaryWithValue()
    {
      Dictionary = new Dictionary<String, String>();
      return Dictionary;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public bool string_to_bool(string str)
    {
      double ret = string_to_double(str);
      return (bool)(ret > 0);
    }

    public double string_to_double(string str)
    {
      double ret = 0;
      if (str != "")
      {
        try
        {
          ret = double.Parse(str);
        }
        catch
        {
        }
      }
      return ret;
    }

    public int string_to_int(string str)
    {
      double ret = string_to_double(str);
      return (int)(ret);
    }
    /// <summary>
    /// Convert bool to Int
    /// </summary>
    /// <param name="input">true/false</param>
    /// <returns>1/0</returns>
    public int bool_to_int(bool input)
    {
      int ret = 0;
      if (input == true)
      {
        ret = 1;
      }
      return ret;
    }
    /// <summary>
    /// Convert input to string
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public string ConvertToString(object input)
    {
      string ret = "";
      if (input is bool)
      {
        ret = (bool_to_int((bool)(input))).ToString();
      }
      else
      {
        ret = input.ToString();
      }
      return ret;
    }
  }
}
