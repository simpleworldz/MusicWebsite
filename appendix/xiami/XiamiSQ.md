## 虾米下载无损音乐方法  

### 工具  
* 虾米音乐PC客户端  
* Fiddler 抓包工具  

### 步骤  
#### 1.打开Fiddler抓包工具，然后用虾米客户端关键词搜索（无需登录） 
<img src="https://github.com/simpleworldz/MusicWebsite/blob/master/appendix/xiami/1.PNG" width="650" />  

#### 2.在Fiddler中找到对应的请求（一共就没几个请求，一目了然）  
<img src="https://github.com/simpleworldz/MusicWebsite/blob/master/appendix/xiami/2.PNG" width="950" />  
第一次使用可能会提示安装证书，这是用于解密https数据的  

**尝试过以抓取到的请求报文头及cookie发送请求，能用，但是第二天一早发现cookie过期了**  

#### 3.从返回json数据中找到歌曲url，各种音质、格式的都有  
<img src="https://github.com/simpleworldz/MusicWebsite/blob/master/appendix/xiami/3.PNG" />
