import AppGlobal from '../configs/models/ConfigInfoModel';
import { SystemSetting } from '../configs/constants/SystemSetting';
import CryptoJs from 'crypto-js';
import axios from 'axios';

export function Post(api, data) {
    let token = AppGlobal.userInfo.token === null || AppGlobal.userInfo.token === '' ?
        localStorage.getItem("token") === null ? '' : localStorage.getItem("token") :
        AppGlobal.userInfo.token;
    let sign = '';
    if (token !== '')
        sign = Sign(token);
    data.token = token;
    data.sign = sign;
    return new Promise((resolve, reject) => {
        axios.post(api, data).then((res => {
            resolve(res);
        })).catch((reason) => {
            console.log('error', reason);
            reject(reason);
        })
    })
}
//sign
function Sign(token) {
    let params = [];
    params.push(token);
    let key = SystemSetting.serviceKey;
    params.push(key);
    params = params.sort();
    let utf8Params = CryptoJs.enc.Utf8.parse(params.join('').toUpperCase());
    let sign = CryptoJs.MD5(utf8Params).toString(CryptoJs.enc.Hex).substring(5, 29);
    return sign;
}