// pages/message/index.js
const Api = require('../../utils/httpPost');
const constant = require('../../configs/constants');
Page({
  /**
   * 页面的初始数据
   */
  data: {
    categories: [],
    banners: [],
    info: [],
    activeCategoryId: 0,
    swiperCurrent: 0,
    curPage: 1,
    pageSize: 20,
    loadingHiden: true
  },
  toDetail: function(e) {
    let id = e.currentTarget.dataset.id;
    let navigateUrl = `../message/detail/detail?id=${id}`;
    wx.navigateTo({
      url: navigateUrl
    })
  },
  tabClick: function(e) {
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
    this.getMessageList(e.currentTarget.id, false)
  },
  swiperchange: function(e) {
    this.setData({
      swiperCurrent: e.detail.current
    })
  },
  pubDetail: function() {
    wx.navigateTo({
      url: './pubMessage/pubmessage'
    })
  },
  previewImg: function(e) {
    var index = e.currentTarget.dataset.index;
    var imgArr = e.currentTarget.dataset.picdata;
    wx.previewImage({
      current: imgArr[index], //当前图片地址
      urls: imgArr, //所有要预览的图片的地址集合 数组形式
      success: function(res) {},
      fail: function(res) {},
      complete: function(res) {},
    })
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function(options) {
    Api.Post('/api/Banners', {
      types: 1
    }).then(res => {
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
    });
  },

  onShow: function() {
    Api.Post('/api/MessageType', {}).then(res => {
      let tempArry = [];
      if (res.code == 200) {
        res.data.map((v) => {
          let tempObj = {};
          tempObj.icon = `${constant.baseUrl}${v.pic}`;
          tempObj.id = v.types;
          tempObj.name = v.title;
          tempArry.push(tempObj);
        });
        this.setData({
          categories: tempArry
        })
      }
    });
    this.getMessageList(this.data.activeCategoryId,false);
  },
  onReachBottom: function() {
    this.setData({
      curPage: this.data.curPage + 1
    });
    this.getMessageList(this.data.activeCategoryId, true)
  },
  onPullDownRefresh: function() {
    this.setData({
      curPage: 1
    });
    this.getMessageList(this.data.activeCategoryId, false)
    wx.stopPullDownRefresh()
  },
  getMessageList: function(type, append) {
    var that = this;
    wx.showLoading({
      "mask": true
    })
    Api.Post('/api/Message', {
      pageIndex: this.data.curPage,
      types: type
    }).then(function(res) {
      wx.hideLoading()
      if (res.data.length == 0) {
        let newData = {
          loadingHiden: false
        }
        if (!append) {
          newData.info = []
        }
        that.setData(newData);
        return;
      }
      let tempArry = [];
      if (append) {
        tempArry = that.data.info
      }
      res.data.map((v) => {
        let tempObj = v;
        let tempArry2 = [];
        var picsArr = JSON.parse(v.pics);
        picsArr.map((v) => {
          let picObj = `${constant.baseUrl}${v}`;
          tempArry2.push(picObj)
        });
        tempObj.pics = tempArry2;
        tempArry.push(tempObj);
      });
      that.setData({
        loadingHiden: true,
        info: tempArry,
      });
    })
  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function() {

  }
})