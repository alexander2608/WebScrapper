<template>
  <form>
      <h1> Result </h1>
      <li v-for="webScraperResult in GetWebScraperResultResponse.webScraperResultList" :key="webScraperResult.position" class="result">
          <span v-bind:class="{'match' : webScraperResult.isMatch == true}">{{webScraperResult.position}} - {{webScraperResult.address}}</span>
      </li>
      <h3 v-if="GetWebScraperResultResponse.isSuccess === false">{{GetWebScraperResultResponse.message}}</h3>
  </form>
</template>

<script>
    import ClientInput from './ClientInput.vue';
    export default{
        data(){
            return{
                
            }
        },
        computed:{
            GetWebScraperResultResponse(){
                return this.$store.getters.GetWebScraperResultResponse;
            }
        }
    }
</script>

<style scoped>
.item {
  margin-top: 2rem;
  display: flex;
}
.form {
    min-width: 400px;
    margin: 30px auto;
    background:gray;
    text-align: left;
    padding: 40px;
    border-radius: 50px;
}
.match{
    color: yellow;
}
.result{
    color: white
}
</style>
