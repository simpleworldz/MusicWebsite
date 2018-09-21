using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHelpers.Helpers
{
    public class MusicInfo
    {//$radio_songs[] = [
     //                      'type'   => 'kg',
     //                      'link'   => 'https://kg.qq.com/node/play?s=' . $radio_song_id. '&shareuid='. $radio_data['uid'],
     //                    'songid' => $radio_song_id,
     //                    'title'  => $radio_data['song_name'],
     //                    'author' => $radio_data['nick'],
     //                    'lrc'    => $radio_lrc_info['data']['lyric'],
     //                    'url'    => $radio_data['playurl'],
     //                    'pic'    => $radio_data['cover']
     //                ];
        public string type { get;set;}
        public string link { get;set;}
        public string songid { get;set;}
        public string title { get;set;}
        public string author { get;set;}
        public string lrc { get;set;}
        public string url { get;set;}
        public string pic { get;set;}
    }
}
