<template>
  <form>
    <h1>Web Scraper</h1>
    <label>Phrase:</label>

    <input type="Phrase" v-model="Phrase">
    <label>URL:</label>

    <input type="url" v-model="Url">
    <button type="button" @click="Submit">Submit</button>

  </form>
</template>

<script>
import ScraperResultList from './ScraperResultList.vue'

export default {
    data(){
        return{
            Phrase: ''
            ,Url: ''
        }

    },
    methods:{
        Submit: function(){
            this.SetUrl();
            this.SetPhrase();
            this.GetWebScraperResult();
        },
        GetWebScraperResult: function(){
            this.$store.dispatch("GetWebScraperResult")
        },
        SetPhrase: function(){
            this.$store.commit("SetPhrase",this.Phrase)
        },
        SetUrl: function(){
            this.$store.commit("SetUrl",this.Url)
        }
    }
}
</script>

<style>
form {
    max-width: 600px;
    min-width: 400px;
    margin: 30px auto;
    background:gray;
    text-align: left;
    padding: 40px;
    border-radius: 50px;
}
label {
    color:white;
    display: inline-block;
    margin: 25px 0 15px ;
    font-size: 1em;
    text-transform: uppercase;
    letter-spacing: 1px;
    font-weight: bold;
}
input{
    display: block;
    padding: 10px 6px;
    width: 100%;
    box-sizing: border-box;
    border: none;
    border-bottom: 1px solid #ddd;
    color: #555;
}
button{
    padding: 10px 6px;
    width: 40%;
    margin: 30px 0 15px;
}
h1{
    color: white;
}

</style>