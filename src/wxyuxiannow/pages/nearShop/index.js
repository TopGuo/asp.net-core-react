// pages/nearShop/index.js
const Api = require('../../utils/httpPost');
const constant = require('../../configs/constants');
Page({

  /**
   * 页面的初始数据
   */
  data: {
    inputShowed: false,
    curPage: 1,
    inputVal: "", // 搜索框内容
    banners: [],
    shops: [
      {
        "id": 1,
        "pic": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg",
        "name": "londis美发店",
        "content": "美发店啦啦啦",
        "lookCount": 300,
        "type": 1,
        "far": 12.6,
        "location": "河北省石家庄市裕华区同祥城",
        "telphone": "18333103619",
        "latitude": 85,
        "longitude": 100,
        "openTime": "8:00",
        "closeTime": "22:00"
      }
    ],
    dataList: [],
    indicatorDots: true,
    autoplay: true,
    interval: 5000,
    duration: 1000,
    circular: true,
    sideMargin: '1rpx',
    showLoading: false,
    loadingHiden: true,
    longitude: 0,
    latitude: 0
  },
  toShopDetail: function (options) {
    let value = options.currentTarget.dataset;
    let shopInfo = JSON.stringify(value.item);
    let navigateUrl = `../nearShop/shopDetail/index?shopInfo=${shopInfo}`;
    wx.navigateTo({
      url: navigateUrl
    })
  },
  // 以下为搜索框事件
  showInput: function () {
    this.setData({
      inputShowed: true
    });
  },
  hideInput: function () {
    this.setData({
      inputVal: "",
      inputShowed: false
    });
  },
  clearInput: function () {
    this.setData({
      inputVal: ""
    });
  },
  inputTyping: function (e) {
    this.setData({
      inputVal: e.detail.value
    });
  },
  toSearch: function () {
    console.log('search...', this.data.inputVal)
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    Api.Post('/api/Announces', {}).then(res => {
      if (res.code == 200) {
        this.setData({
          dataList: res.data
        })
      }
    });
    Api.Post('/api/Banners', { types: 0 }).then(res => {
      let tempArry = [];
      if (res.code == 200) {
        res.data.map((v) => {
          let tempObj = {};
          tempObj.pic = `${constant.baseUrl}${v.pic}`;
          tempArry.push(tempObj);
        });
        this.setData({
          banners: tempArry
        })
      }
    })
  },
  onShow: function () {
    let that = this;
    wx.getLocation({
      type: "gcj02",
      success(res) {
        const latitude = res.latitude
        const longitude = res.longitude
        that.setData({
          longitude: longitude,
          latitude: latitude
        })
        that.getShopList(false);
      }
    })

  },
  onReachBottom: function () {
    this.setData({
      curPage: this.data.curPage + 1
    });
    this.getShopList(true)
  },
  onPullDownRefresh: function () {
    this.setData({
      curPage: 1
    });
    this.getShopList(false)
    wx.stopPullDownRefresh()
  },
  getShopList: function (append) {
    var that = this;
    wx.showLoading({
      "mask": true
    })
    Api.Post('/api/Shops', {
      pageIndex: this.data.curPage,
      longitude: that.data.longitude,
      latitude: that.data.latitude
    }).then(function (res) {
      wx.hideLoading()
      if (res.data.length == 0) {
        let newData = {
          loadingHiden: false
        }
        if (!append) {
          newData.shops = []
        }
        that.setData(newData);
        return
      }
      let tempArry = [];
      if (append) {
        tempArry = that.data.shops
      }
      res.data.map((v) => {
        let tempObj = v;
        tempObj.logoPic = `${constant.baseUrl}${v.logoPic}`;
        tempObj.distance = v.distance.toFixed(3)
        tempArry.push(tempObj)
      });
      that.setData({
        loadingHiden: true,
        shops: tempArry,
      });
    })
  }
})