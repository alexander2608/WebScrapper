import { createApp } from 'vue'
import Vuex from 'vuex'
export default new Vuex.Store({
    state: {
        GetWebScraperResultResponse:{
            isSuccess: false,
            message: "",
            webScraperResultList:[]
        },
        Phrase:"",
        Url:"",
        config: null
    },
    mutations:{
        SetGetWebScraperResultResponse(state,payload){
            state.GetWebScraperResultResponse = payload;
        },
        SetPhrase(state, payload){
            state.Phrase = payload;
        },
        SetUrl(state, payload){
            state.Url = payload;
        },
        SetConfig(state, payload){
            state.config = payload;
        }
    },
    actions:{
        async GetWebScraperResult(state,payload){
            const requestOptions = {
                method: "POST",
                headers: { "content-type": "application/json" },
                body: JSON.stringify(
                    { 
                        "phrase": this.state.Phrase,
                        "url": this.state.Url 
                    })
            };
            var serverUrl = this.state.config.serverUrl;
            await fetch(serverUrl+"api/webScrapper/GetWebScrapper", requestOptions)
            .then(response => response.json())
            .then(data => state.commit("SetGetWebScraperResultResponse", data))
            .catch(err=>console.log(err.message))
        }
    },
    getters: {
        GetWebScraperResultResponse(state){
            return state.GetWebScraperResultResponse;
        },
        
        GetPhrase(state){
            return state.Phrase;
        },
        GetUrl(state){
            return state.Url;
        }
    }
})
