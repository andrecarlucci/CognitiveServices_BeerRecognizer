namespace Untappd.Models
{

    public class Rootobject
    {
        public Meta Meta { get; set; }
        public object[] notifications { get; set; }
        public Response Response { get; set; }
    }

    public class Response
    {
        public Pagination Pagination { get; set; }
        public CheckinList Checkins { get; set; }
    }

    public class Pagination
    {
        public string since_url { get; set; }
        public string next_url { get; set; }
        public string max_id { get; set; }
    }

    public class CheckinList
    {
        public int count { get; set; }
        public Checkin[] Items { get; set; }
    }

    public class Checkin
    {
        public int checkin_id { get; set; }
        public string created_at { get; set; }
        public string checkin_comment { get; set; }
        public float rating_score { get; set; }
        public User user { get; set; }
        public Beer beer { get; set; }
        public Brewery brewery { get; set; }
        public object venue { get; set; }
        public Comments comments { get; set; }
        public Toasts toasts { get; set; }
        public Media Media { get; set; }
        public Source source { get; set; }
        public Badges badges { get; set; }
    }

    public class User
    {
        public int uid { get; set; }
        public string user_name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string location { get; set; }
        public string url { get; set; }
        public int is_supporter { get; set; }
        public string bio { get; set; }
        public object relationship { get; set; }
        public string user_avatar { get; set; }
        public int is_private { get; set; }
    }

    public class Brewery
    {
        public int brewery_id { get; set; }
        public string brewery_name { get; set; }
        public string brewery_slug { get; set; }
        public string brewery_page_url { get; set; }
        public string brewery_label { get; set; }
        public string country_name { get; set; }
        public Contact contact { get; set; }
        public Location location { get; set; }
        public int brewery_active { get; set; }
    }

    public class Contact
    {
        public string twitter { get; set; }
        public string facebook { get; set; }
        public string url { get; set; }
    }

    public class Location
    {
        public string brewery_city { get; set; }
        public string brewery_state { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
    }

    public class Comments
    {
        public int total_count { get; set; }
        public int count { get; set; }
        public object[] items { get; set; }
    }

    public class Toasts
    {
        public int total_count { get; set; }
        public int count { get; set; }
        public bool? auth_toast { get; set; }
        public Item1[] items { get; set; }
    }

    public class Item1
    {
        public int uid { get; set; }
        public User1 user { get; set; }
        public int like_id { get; set; }
        public bool like_owner { get; set; }
        public string created_at { get; set; }
    }

    public class User1
    {
        public int uid { get; set; }
        public string user_name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string bio { get; set; }
        public string location { get; set; }
        public string relationship { get; set; }
        public string user_avatar { get; set; }
        public string account_type { get; set; }
    }

    public class Media
    {
        public int count { get; set; }
        
        public PhotoItem[] Items { get; set; }
    }

    public class PhotoItem
    {
        public int photo_id { get; set; }
        public Photo Photo { get; set; }
    }

    public class Photo
    {
        public string photo_img_sm { get; set; }
        public string Photo_img_md { get; set; }
        public string photo_img_lg { get; set; }
        public string photo_img_og { get; set; }
    }

    public class Source
    {
        public string app_name { get; set; }
        public string app_website { get; set; }
    }

    public class Badges
    {
        public bool retro_status { get; set; }
        public int count { get; set; }
        public Item3[] items { get; set; }
    }

    public class Item3
    {
        public int badge_id { get; set; }
        public int user_badge_id { get; set; }
        public string badge_name { get; set; }
        public string badge_description { get; set; }
        public string created_at { get; set; }
        public Badge_Image badge_image { get; set; }
    }

    public class Badge_Image
    {
        public string sm { get; set; }
        public string md { get; set; }
        public string lg { get; set; }
    }


}
