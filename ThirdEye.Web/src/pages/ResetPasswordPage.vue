<template>
    <div class="page">
        <form class="backplate">
            <div class="header">
                <div class="text">
                    Reset password
                </div>
            </div>
            <div class="content">
                <div class="inputs">
                    <third-eye-input type="password" holder="Password" v-model="password"></third-eye-input>
                    <third-eye-input type="password" holder="Repeat password" v-model="repeatPassword"></third-eye-input>
                </div>
                <third-eye-button class="btn" type="submit">
                    OK
                </third-eye-button>
            </div>
        </form>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                password: '',
                repeatPassword: '',
            }
        }
    }
</script>

<style scoped>

    .page {
        top: 0;
        bottom: 0;
        right: 0;
        left: 0;
        position: fixed;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .backplate {
        background-color: white;
        border-radius: 30px;
        width: 300px;
        min-height: 50px;
    }

    .content {
        display: flex;
        padding:30px;
        flex-direction: column;
        justify-items: center;
        align-items: center;
    }

    .inputs {
        display: flex;
        width: 100%;
        flex-direction: column;
    }

    .btn {
        margin-top: 20px;
        height: 40px;
        width: 40%;
    }

    .header{
        display: flex;
        align-items: center;
        justify-content: center;
        height: 50px;
        background-color: #6f38ac;
        border-radius: 30px 30px 0 0;
    }

    .text {
        text-align: center;
        font-size: 20px;
        font-family: 'Segoe UI';
        color: white;
    }
</style>