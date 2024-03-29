namespace Artworks_Sharing_Plaform_Api.Enum
{
    public class ServerErrorEnum
    {
        public const string SERVER_ERROR = "SERVER_ERROR";
        public const string NOT_AUTHENTICATED = "NOT_AUTHENTICATED";
        public const string NOT_AUTHORIZED = "NOT_AUTHORIZED";
        public const string MISSING_AURGUMENT = "MISSING_AURGUMENT";
        public const string STATUS_NOT_FOUND = "STATUS_NOT_FOUND";
    }

    public class AccountErrorEnum
    {
        public const string ACCOUNT_NOT_FOUND = "ACCOUNT_NOT_FOUND";
        public const string ACCOUNT_EXISTED = "ACCOUNT_EXISTED";
        public const string ACCOUNT_UPDATE_FAILED = "ACCOUNT_UPDATE_SUCCESS";
        public const string LOGIN_FAILED = "LOGIN_FAILED";
        public const string ACCOUNT_MEMBER_NOT_FOUND = "ACCOUNT_MEMBER_NOT_FOUND";
        public const string ACCOUNT_CREATOR_NOT_FOUND = "ACCOUNT_CREATOR_NOT_FOUND";
    }

    public class ArtWorkErrorEnum
    {
        public const string ARTWORK_NOT_FOUND = "ARTWORK_NOT_FOUND";
    }

    public class RoleErrorEnum
    {
        public const string ROLE_NOT_FOUND = "ROLE_NOT_FOUND";
    }

    public class LikeByErrorEnum
    {
        public const string LIKE_BY_ADD_FAIL = "LIKE_BY_ADD_FAIL";
    }

    public class RequestArtworkErrorEnum
    {
        public const string REQUEST_USER_NOT_FOUND = "REQUEST_USER_NOT_FOUND";
        public const string CREATOR_USER_NOT_FOUND = "CREATOR_USER_NOT_FOUND";
        public const string STATUS_NOT_FOUND = "STATUS_NOT_FOUND";
        public const string UPDATE_REQUEST_ARTWORK_STATUS_FAIL = "UPDATE_REQUEST_ARTWORK_STATUS_FAIL";
    }

    public class ComplantErrorNum
    {
        public const string COMPLANT_ACCOUNT_NOT_FOUND = "COMPLANT_ACCOUNT_NOT_FOUND";
        public const string MANAGE_ISSUES_ACCOUNT_NOT_FOUND = "MANAGE_ISSUES_ACCOUNT_NOT_FOUND";
        public const string COMPLANT_NOT_FOUND = "COMPLANT_NOT_FOUND";
        public const string COMPLANT_UPDATE_FAILED = "COMPLANT_UPDATE_FAILED";
        public const string COMPLANT_CREATE_FAILED = "COMPLANT_CREATE_FAILED";
        public const string COMPLANT_STATUS_NOT_FOUND = "COMPLANT_STATUS_NOT_FOUND";
    }
   
    public class UserFollowerErrorEnum
    {
        public const string REQUEST_USER_NOT_FOUND = "REQUEST_USER_NOT_FOUND";
        public const string CREATOR_USER_NOT_FOUND = "CREATOR_USER_NOT_FOUND";
    }

    public class CommentErrorEnum
    {
        public const string COMMENT_NOT_FOUND = "COMMENT_NOT_FOUND";
    }

    public class ArtworkTypeErrorNum
    {
        public const string CREATE_ARTWORK_TYPE_FAIL = "CREATE_ARTWORK_TYPE_FAIL";
        public const string UPDATE_ARTWORK_TYPE_FAIL = "UPDATE_ARTWORK_TYPE_FAIL";
        public const string TYPE_OF_ARTWORK_NOT_FOUND = "TYPE_OF_ARTWORK_NOT_FOUND";
        public const string ARTWORK_NOT_FOUND = "ARTWORK_NOT_FOUND";
    }

    public class PostArtworkErrorEnum
    {
        public const string POST_ARTWORK_NOT_FOUND = "POST_ARTWORK_NOT_FOUND";
    }
    public class PostErrorEnum
    {
        public const string POST_NOT_FOUND = "POST_NOT_FOUND";
    }

    public class SharingPostArtworkErrorEnum
    {
        public const string SHARING_POST_ARTWORK_FAIL = "SHARING_POST_ARTWORK_FAIL";
    }

    public class TypeOfArtworkErrorEnum
    {
        public const string TYPE_OF_ARTWORK_NOT_FOUND = "TYPE_OF_ARTWORK_NOT_FOUND";
        public const string TYPE_OF_ARTWORK_STATUS_NOT_FOUND = "TYPE_OF_ARTWORK_STATUS_NOT_FOUND";
    }
}
