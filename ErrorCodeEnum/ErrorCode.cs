namespace LoginServer.ErrorCodeEnum
{
    public enum ErrorCode
    {
        WhiteSpace = -1,    // 공백
        Succeess = 0,
        Fail = 1,
        Duplication = 2,  // 중복
        NotDuplication = 3,
        NotFoundUserInfo = 4,
        NotFoundPassword = 5,
        Loging = 6,
        NotLoging = 7,
        LogoutSuccess = 8
    }
}