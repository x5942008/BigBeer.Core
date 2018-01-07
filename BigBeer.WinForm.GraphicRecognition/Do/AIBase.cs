namespace BigBeer.WinForm.GraphicRecognition.Do
{
    public static class AI
    {
        // 设置APPID/AK/SK
        //static string APP_ID = "你的 App ID";
        static string API_KEY = "vGgwbTEi6iEASwUh5uw8Eq25";
        static string SECRET_KEY = "uZeTaYeZZ5GoxxWihYWVVUau5scHEH0l";
        public static Baidu.Aip.Ocr.Ocr client => new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
    }
}
