// pages/message/index.js
const Api = require('../../utils/httpPost');
const constant = require('../../configs/constants');
Page({
  /**
   * 页面的初始数据
   */
  data: {
    categories: [
      {
        "id": 1,
        "icon": "../../images/m_pic/job.png",
        "name": "求职招聘"
      },
      {
        "id": 2,
        "icon": "../../images/m_pic/hourse.png",
        "name": "房屋信息"
      },
      {
        "id": 3,
        "icon": "../../images/m_pic/carchange.png",
        "name": "二手汽车"
      },
      {
        "id": 4,
        "icon": "../../images/m_pic/youhuixinxi.png",
        "name": "优惠信息"
      },
      {
        "id": 5,
        "icon": "../../images/m_pic/chatfrind.png",
        "name": "征婚交友"
      },
      {
        "id": 6,
        "icon": "../../images/m_pic/local_server.png",
        "name": "其他服务"
      }
    ],
    banners: [
      {
        "id": 0,
        "pic": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg"
      },
      {
        "id": 1,
        "pic": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg"
      },
      {
        "id": 2,
        "pic": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg"
      }
    ],
    info: [
      {
        "id": 1,
        "pic": "",
        "type": 1,
        "title": "花儿为什么这样红",
        "content": "花有红的黄的蓝的绿的...花有红的黄的蓝的绿的花有红的黄的蓝的绿的花有红的黄的蓝的绿的...花有红的黄的蓝的绿的..花有红的黄的蓝的绿的..花有红的黄的蓝的绿的...花有红的黄的蓝的绿的...花有红的黄的蓝的绿的...花有红的黄的蓝的绿的...花有红的黄的蓝的绿的花有红的黄的蓝的绿的...花有红的黄的蓝的绿的花有红的黄的蓝的绿的花有红的黄的蓝的绿的...花有红的黄的蓝的绿的..花有红的黄的蓝的绿的..花有红的黄的蓝的绿的...花有红的黄的蓝的绿的...花有红的黄的蓝的绿的...花有红的黄的蓝的绿的...花有红的黄的蓝的绿的",
        "time": "2019-11-5 12:11",
        "lookCount": 100
      },
      {
        "id": 2,
        "pic": [
          'http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg',
          'http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg',
          'http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg',
          'http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg',
          'http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg'
        ],
        "type": 1,
        "title": "花儿为什么这样红2",
        "content": "小荷才露尖尖角 草有蜻蜓立枝头",
        "time": "2019-11-5 12:11",
        "lookCount": 100
      },
      {
        "id": 3,
        "pic": "",
        "type": 1,
        "title": "花儿为什么这样红3",
        "content": "",
        "time": "2019-11-5 12:11",
        "lookCount": 100
      }
    ],
    activeCategoryId: 0,
    swiperCurrent: 0,
    curPage: 1,
    pageSize: 20,
    loadingMoreHidden: true
  },
  tabClick: function (e) {
    let offset = e.currentTarget.offsetLeft;
    if (offset > 150) {
      offset = offset - 150
    } else {
      offset = 0;
    }
    this.setData({
      activeCategoryId: e.currentTarget.id,
      curPage: 1,
      cateScrollTop: offset
    });
  },
  swiperchange: function (e) {
    this.setData({
      swiperCurrent: e.detail.current
    })
  },
  pubDetail: function () {
    wx.navigateTo({
      url: './pubMessage/pubmessage'
    })
  },
  previewImg: function (e) {
    var index = e.currentTarget.dataset.index;
    var imgArr = e.currentTarget.dataset.picdata;
    wx.previewImage({
      current: imgArr[index],     //当前图片地址
      urls: imgArr,               //所有要预览的图片的地址集合 数组形式
      success: function (res) { },
      fail: function (res) { },
      complete: function (res) { },
    })
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    Api.Post('/api/Banners', { types: 1 }).then(res => {
      let tempArry = [];
      if (res.code == 200) {
        res.data.map((v) => {
          let tempObj = {};
          tempObj.pic = `${constant.baseUrl}${v.pic}`;
          tempObj.id = v.id;
          tempArry.push(tempObj);
        });
        this.setData({
          banners: tempArry
        })
      }
    })
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})